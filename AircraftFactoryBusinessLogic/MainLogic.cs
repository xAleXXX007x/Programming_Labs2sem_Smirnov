using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;

        private readonly IStockLogic stockLogic;

        private readonly IAircraftLogic aircraftLogic;

        public MainLogic(IOrderLogic orderLogic, IStockLogic stockLogic, IAircraftLogic aircraftLogic)
        {
            this.orderLogic = orderLogic;
            this.stockLogic = stockLogic;
            this.aircraftLogic = aircraftLogic;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                AircraftId = model.AircraftId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            var aircraft = aircraftLogic.GetElement(order.AircraftId);
            var stocks = stockLogic.GetList();

            foreach (var part in aircraft.AircraftParts)
            {
                int count = 0;

                foreach (var stock in stocks)
                {
                    foreach (var stockPart in stock.StockParts)
                    {
                        if (stockPart.PartId.Equals(part.PartId))
                        {
                            count += stockPart.Count;
                        }
                    }
                }

                if (count < part.Count * order.Count)
                {
                    throw new Exception("Недостаточно запчастей \"" + part.PartName + "\" для выполнения заказа");
                } else
                {
                    int partsLeft = part.Count * order.Count;

                    foreach (var stock in stocks)
                    {
                        List<StockPartViewModel> delete = new List<StockPartViewModel>();

                        foreach (var stockPart in stock.StockParts)
                        {
                            if (partsLeft > 0)
                            {
                                if (stockPart.PartId.Equals(part.PartId))
                                {
                                    if (stockPart.Count <= partsLeft)
                                    {
                                        partsLeft -= stockPart.Count;

                                        stockPart.Count = 0;

                                        if (partsLeft == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        stockPart.Count -= partsLeft;
                                        partsLeft = 0;

                                        continue;
                                    }
                                }
                            }
                        }

                        if (partsLeft == 0)
                        {
                            continue;
                        }
                    }
                }
            }

            foreach (var stock in stocks)
            {
                List<StockPartBindingModel> parts = new List<StockPartBindingModel>();

                foreach (var part in stock.StockParts)
                {
                    parts.Add(new StockPartBindingModel
                    {
                        Id = part.Id,
                        StockId = part.StockId,
                        PartId = part.PartId,
                        Count = part.Count
                    });
                }

                stockLogic.UpdElement(new StockBindingModel
                {
                    Id = stock.Id,
                    StockName = stock.StockName,
                    StockParts = parts
                });
            }

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
        }

        public void RefillStock(StockBindingModel model, StockPartBindingModel partModel)
        {
            var stock = stockLogic.GetElement(model.Id);
            if (stock == null)
            {
                throw new Exception("Не найден склад");
            }

            List<StockPartBindingModel> stockParts = model.StockParts;
            bool found = false;
            foreach (StockPartBindingModel stockPart in stockParts)
            {
                if (stockPart.PartId == partModel.PartId)
                {
                    stockPart.Count += partModel.Count;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                stockParts.Add(partModel);
            }

            stockLogic.UpdElement(new StockBindingModel
            {
                Id = stock.Id,
                StockName = stock.StockName,
                StockParts = stockParts
            });
        }
    }
}
