using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryListImplement
{
    public class MainLogic : IMainLogic
    {
        private readonly DataListSingleton source;

        public MainLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetOrders()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            for (int i = 0; i < source.Orders.Count; ++i)
            {
                string aircraftName = string.Empty;

                for (int j = 0; j < source.Aircrafts.Count; ++j)
                {
                    if (source.Aircrafts[j].Id == source.Orders[i].AircraftId)
                    {
                        aircraftName = source.Aircrafts[j].AircraftName;

                        break;
                    }
                }

                result.Add(new OrderViewModel
                {
                    Id = source.Orders[i].Id,
                    AircraftId = source.Orders[i].AircraftId,
                    AircraftName = aircraftName,
                    Count = source.Orders[i].Count,
                    Sum = source.Orders[i].Sum,
                    DateCreate = source.Orders[i].DateCreate,
                    DateImplement = source.Orders[i].DateImplement,
                    Status = source.Orders[i].Status
                });
            }

            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = 0;

            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id > maxId)
                {
                    maxId = source.Orders[i].Id;
                }
            }

            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                AircraftId = model.AircraftId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            if (source.Orders[index].Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            source.Orders[index].DateImplement = DateTime.Now;
            source.Orders[index].Status = OrderStatus.Выполняется;
        }

        public void FinishOrder(OrderBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    index = i;

                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            if (source.Orders[index].Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }

            source.Orders[index].Status = OrderStatus.Готов;
        }

        public void PayOrder(OrderBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    index = i;

                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            if (source.Orders[index].Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }

            source.Orders[index].Status = OrderStatus.Оплачен;
        }
    }
}
