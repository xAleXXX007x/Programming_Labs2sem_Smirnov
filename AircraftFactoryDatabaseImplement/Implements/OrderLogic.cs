using AircraftFactoryBusinessLogic.BindingModels;
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
                    result.Add(CreateViewModel(context.Orders.Include(rec => rec.Aircraft).FirstOrDefault(rec => rec.Id == model.Id)));
                }
                else
                {
                    result.AddRange(context.Orders.Select(rec => CreateViewModel(rec)));
                }
                return result;
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Aircraft aircraft = context.Aircrafts.Where(rec => rec.Id == model.AircraftId).FirstOrDefault();

                if (aircraft == null)
                {
                    throw new Exception("Элемент не найден");
                }

                order.AircraftId = model.AircraftId;
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
