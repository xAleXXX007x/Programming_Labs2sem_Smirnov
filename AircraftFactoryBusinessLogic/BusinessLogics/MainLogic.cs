using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.BusinessLogics;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.HelperModels;
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

        private readonly IClientLogic clientLogic;

        private readonly IStockLogic stockLogic;

        public MainLogic(IOrderLogic orderLogic, IStockLogic stockLogic, IClientLogic clientLogic)
        {
            this.orderLogic = orderLogic;
            this.clientLogic = clientLogic;
            this.stockLogic = stockLogic;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                AircraftId = model.AircraftId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel
                {
                    Id = model.ClientId
                }).FirstOrDefault().Email,
                Subject = $"Новый заказ",
                Text = $"Заказ принят."
            });
        }

        private readonly object locker = new object();

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.ТребуютсяМатериалы)
                {
                    throw new Exception("Заказ не в статусе \"Принят\" или \"Требуются Материалы\"");
                }
                if (order.ImplementerId != null)
                {
                    throw new Exception("Заказ уже занят");
                }

                var suffParts = stockLogic.WithdrawStock(order);

                if (suffParts)
                {
                    orderLogic.CreateOrUpdate(new OrderBindingModel
                    {
                        Id = order.Id,
                        AircraftId = order.AircraftId,
                        ClientId = order.ClientId,
                        ClientFIO = order.ClientFIO,
                        ImplementerId = model.ImplementerId,
                        Count = order.Count,
                        Sum = order.Sum,
                        DateCreate = order.DateCreate,
                        DateImplement = DateTime.Now,
                        Status = OrderStatus.Выполняется
                    });

                    MailLogic.MailSendAsync(new MailSendInfo
                    {
                        MailAddress = clientLogic.Read(new ClientBindingModel
                        {
                            Id = order.ClientId
                        })?[0]?.Email,
                        Subject = $"Заказ №{order.Id}",
                        Text = $"Заказ №{order.Id} передан в работу."
                    });
                } else
                {
                    orderLogic.CreateOrUpdate(new OrderBindingModel
                    {
                        Id = order.Id,
                        AircraftId = order.AircraftId,
                        ClientId = order.ClientId,
                        ClientFIO = order.ClientFIO,
                        Count = order.Count,
                        Sum = order.Sum,
                        DateCreate = order.DateCreate,
                        Status = OrderStatus.ТребуютсяМатериалы
                    });
                }
            }
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
                ClientId = order.ClientId,
                ClientFIO = order.ClientFIO,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel
                {
                    Id = order.ClientId
                })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} готов."
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
                ClientId = order.ClientId,
                ClientFIO = order.ClientFIO,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel
                {
                    Id = order.ClientId
                })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} оплачен."
            });
        }
    }
}
