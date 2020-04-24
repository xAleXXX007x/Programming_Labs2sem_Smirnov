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
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Client tempClient = model.Id.HasValue ? null : new Client();

                if (model.Id.HasValue)
                {
                    tempClient = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                }

                if (model.Id.HasValue)
                {
                    if (tempClient == null)
                    {
                        throw new Exception("Элемент не найден");
                    }

                    CreateModel(model, tempClient);
                }
                else
                {
                    context.Clients.Add(CreateModel(model, tempClient));
                }

                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                List<ClientViewModel> result = new List<ClientViewModel>();

                if (model != null)
                {
                    result.AddRange(context.Clients
                        .Where(rec => (rec.Email == model.Email && rec.Password == model.Password))
                        .Select(rec => CreateViewModel(rec)));
                }
                else
                {
                    result.AddRange(context.Clients.Select(rec => CreateViewModel(rec)));
                }
                return result;
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                client.ClientFIO = model.ClientFIO;
                client.Email = model.Email;
                client.Password = model.Password;

                return client;
            }
        }

        static private ClientViewModel CreateViewModel(Client client)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                return new ClientViewModel
                {
                    Id = client.Id,
                    ClientFIO = client.ClientFIO,
                    Email = client.Email,
                    Password = client.Password
                };
            }
        }
    }
}
