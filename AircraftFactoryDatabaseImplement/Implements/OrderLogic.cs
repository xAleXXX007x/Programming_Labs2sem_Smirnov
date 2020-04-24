using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Order tempOrder = model.Id.HasValue ? null : new Order();

                if (model.Id.HasValue)
                {
                    tempOrder = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
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
                    context.Orders.Add(CreateModel(model, tempOrder));
                }

                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                List<OrderViewModel> result = new List<OrderViewModel>();

                if (model != null)
                {
                    result.AddRange(context.Orders.Include(rec => rec.Aircraft)
                        .Where(rec => (model.Id.HasValue && rec.Id == model.Id) ||
                                (model.DateFrom.HasValue && model.DateTo.HasValue &&
                                rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) ||
                                (rec.ClientId == model.ClientId) ||
                                (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue) ||
                                (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && 
                                rec.Status == OrderStatus.Выполняется))
                        .Select(rec => CreateViewModel(rec)));
                }
                else
                {
                    result.AddRange(context.Orders.Include(rec => rec.Aircraft).Select(rec => CreateViewModel(rec)));
                }
                return result;
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Aircraft aircraft = context.Aircrafts.Where(rec => rec.Id == model.AircraftId).FirstOrDefault();
                Client client = context.Clients.Where(rec => rec.Id == model.ClientId).FirstOrDefault();
                Implementer implementer = context.Implementers.Where(rec => rec.Id == model.ImplementerId).FirstOrDefault();

                if (aircraft == null || client == null || model.ImplementerId.HasValue && implementer == null)
                {
                    throw new Exception("Элемент не найден");
                }

                order.AircraftId = model.AircraftId;
                order.ClientId = (int)model.ClientId;
                order.ClientFIO = client.ClientFIO;
                order.ImplementerId = model.ImplementerId;
                order.ImplementerFIO = model.ImplementerId.HasValue ? implementer.ImplementerFIO : String.Empty;
                order.Count = model.Count;
                order.Sum = model.Count * aircraft.Price;
                order.Status = model.Status;
                order.DateCreate = model.DateCreate;
                order.DateImplement = model.DateImplement;

                return order;
            }
        }

        static private OrderViewModel CreateViewModel(Order order)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                return new OrderViewModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    ClientFIO = order.ClientFIO,
                    ImplementerId = order.ImplementerId,
                    ImplementerFIO = order.ImplementerFIO,  
                    AircraftId = order.AircraftId,
                    AircraftName = order.Aircraft.AircraftName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement
                };
            }
        }
    }
}
