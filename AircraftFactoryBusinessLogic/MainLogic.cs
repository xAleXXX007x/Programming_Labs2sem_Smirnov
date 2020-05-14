using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.Interfaces;
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

        public MainLogic(IOrderLogic orderLogic, IStockLogic stockLogic)
        {
            this.orderLogic = orderLogic;
            this.stockLogic = stockLogic;
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
