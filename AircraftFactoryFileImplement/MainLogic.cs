using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryFileImplement
{
    public class MainLogic : IMainLogic
    {
        private readonly FileDataListSingleton source;

        public MainLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetOrders()
        {
            List<OrderViewModel> result = source.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                AircraftId = rec.AircraftId,
                AircraftName = source.Aircrafts.Where(recA => recA.Id == rec.AircraftId).FirstOrDefault().AircraftName,
                Count = rec.Count,
                Sum = rec.Sum,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
                Status = rec.Status
            })
            .ToList();
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
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
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Выполняется;
        }

        public void FinishOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }

            element.Status = OrderStatus.Готов;
        }

        public void PayOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }

            element.Status = OrderStatus.Оплачен;
        }
    }
}
