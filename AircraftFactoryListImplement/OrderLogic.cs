using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryListImplement
{
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;

        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };
            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }

        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (model != null)
                {
                    if (order.Id == model.Id)
                    {
                        result.Add(CreateViewModel(order));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(order));
            }
            return result;
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            Aircraft aircraft = null;

            foreach (Aircraft a in source.Aircrafts)
            {
                if (a.Id == model.AircraftId)
                {
                    aircraft = a;
                    break;
                }
            }

            Implementer implementer = null;

            foreach (Implementer a in source.Implementers)
            {
                if (a.Id == model.ImplementerId)
                {
                    implementer = a;
                    break;
                }
            }

            if (aircraft == null || model.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }

            order.AircraftId = model.AircraftId;
            order.ImplementerId = (int)model.ImplementerId;
            order.ImplementerFIO = implementer.ImplementerFIO;
            order.Count = model.Count;
            order.Sum = model.Count * aircraft.Price;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            Aircraft aircraft = null;

            foreach (Aircraft a in source.Aircrafts)
            {
                if (a.Id == order.AircraftId)
                {
                    aircraft = a;
                    break;
                }
            }

            Implementer implementer = null;

            foreach (Implementer a in source.Implementers)
            {
                if (a.Id == order.ImplementerId)
                {
                    implementer = a;
                    break;
                }
            }

            if (aircraft == null || order.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }

            return new OrderViewModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
                AircraftName = aircraft.AircraftName,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = implementer.ImplementerFIO,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
