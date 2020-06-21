using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryFileImplement
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;

        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };
            
            if (!model.Id.HasValue)
            {
                tempOrder.Id = source.Orders.FirstOrDefault(rec => rec.Id >= tempOrder.Id).Id + 1;
            } else
            {
                tempOrder = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
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
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id.Value);
            
            if (element != null)
            {
                source.Orders.Remove(element);
            } else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            if (model != null)
            {
                result.AddRange(source.Orders
                        .Where(rec => (model.Id.HasValue && rec.Id == model.Id) ||
                                (model.DateFrom.HasValue && model.DateTo.HasValue &&
                                rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) ||
                                (rec.ClientId == model.ClientId) ||
                                (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue) ||
                                (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && 
                                rec.Status == OrderStatus.Выполняется))
                        .Select(rec => CreateViewModel(rec)));
            } else
            {
                result.AddRange(source.Orders.Select(rec => CreateViewModel(rec)));
            }
            return result;
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            Aircraft aircraft = source.Aircrafts.Where(rec => rec.Id == model.AircraftId).FirstOrDefault();
            Client client = source.Clients.Where(rec => rec.Id == order.ClientId).FirstOrDefault();
            Implementer implementer = source.Implementers.Where(rec => rec.Id == model.AircraftId).FirstOrDefault();

            if (aircraft == null || client == null || order.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }

            order.AircraftId = model.AircraftId;
            order.ClientId = model.ClientId.Value;
            order.ClientFIO = client.ClientFIO;
            order.ImplementerId = model.ImplementerId;
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
            Aircraft aircraft = source.Aircrafts.Where(rec => rec.Id == order.AircraftId).FirstOrDefault();
            Client client = source.Clients.Where(rec => rec.Id == order.ClientId).FirstOrDefault();
            Implementer implementer = source.Implementers.Where(rec => rec.Id == order.AircraftId).FirstOrDefault();

            if (aircraft == null || client == null || order.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }

            return new OrderViewModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
                AircraftName = aircraft.AircraftName,
                ClientId  = order.ClientId,
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
