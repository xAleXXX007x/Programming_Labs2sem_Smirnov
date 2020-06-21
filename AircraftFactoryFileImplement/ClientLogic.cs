using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryFileImplement
{
    public class ClientLogic : IClientLogic
    {
        private readonly FileDataListSingleton source;

        public ClientLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ClientBindingModel model)
        {
            Client tempClient = model.Id.HasValue ? null : new Client { Id = 1 };

            if (!model.Id.HasValue)
            {
                tempClient.Id = source.Clients.FirstOrDefault(rec => rec.Id >= tempClient.Id).Id + 1;
            }
            else
            {
                tempClient = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
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
                Client client = source.Clients.FirstOrDefault(rec => rec.Email == model.Email);

                if (client != null)
                {
                    throw new Exception("Данный логин занят");
                }

                source.Clients.Add(CreateModel(model, tempClient));
            }
        }

        public void Delete(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id.Value);

            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            List<ClientViewModel> result = new List<ClientViewModel>();

            if (model != null)
            {
                result.Add(CreateViewModel(source.Clients.FirstOrDefault(rec => rec.Id == model.Id)));
            }
            else
            {
                result.AddRange(source.Clients.Select(rec => CreateViewModel(rec)));
            }
            return result;
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Email = model.Email;
            client.Password = model.Password;

            return client;
        }

        private ClientViewModel CreateViewModel(Client client)
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
