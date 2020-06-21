using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
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
                    if ((model.Id.HasValue && order.Id == model.Id) ||
                                (model.DateFrom.HasValue && model.DateTo.HasValue &&
                                order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo) ||
                                (order.ClientId == model.ClientId) ||
                                (model.FreeOrders.HasValue && model.FreeOrders.Value && !order.ImplementerId.HasValue) ||
                                (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId &&
                                order.Status == OrderStatus.Выполняется))
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

            Client client = null;

            foreach (Client c in source.Clients)
            {
                if (c.Id == model.ClientId)
                {
                    client = c;
                    break;
                }
            }

            Implementer implementer = null;

            foreach (Implementer i in source.Implementers)
            {
                if (i.Id == model.ImplementerId)
                {
                    implementer = i;
                    break;
                }
            }

            if (aircraft == null || client == null || model.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }

            order.AircraftId = model.AircraftId;
            order.ClientId = model.ClientId.Value;
            order.ClientFIO = client.ClientFIO;
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

            Client client = null;

            foreach (Client c in source.Clients)
            {
                if (c.Id == order.ClientId)
                {
                    client = c;
                    break;
                }
            }

            Implementer implementer = null;

            foreach (Implementer i in source.Implementers)
            {
                if (i.Id == order.ImplementerId)
                {
                    implementer = i;
                    break;
                }
            }

            if (aircraft == null || client == null || order.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }

            return new OrderViewModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
                AircraftName = aircraft.AircraftName,
                ClientId = order.ClientId,
                ClientFIO = client.ClientFIO,
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
