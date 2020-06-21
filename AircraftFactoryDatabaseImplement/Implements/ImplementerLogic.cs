using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryDatabaseImplement.Implements
{
    public class ImplementerLogic : IImplementerLogic
    {
        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Implementer tempImplementer = model.Id.HasValue ? null : new Implementer();

                if (model.Id.HasValue)
                {
                    tempImplementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                }

                if (model.Id.HasValue)
                {
                    if (tempImplementer == null)
                    {
                        throw new Exception("Элемент не найден");
                    }

                    CreateModel(model, tempImplementer);
                }
                else
                {
                    Implementer implementer = context.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO);

                    if (implementer != null)
                    {
                        throw new Exception("Данный исполнитель уже есть в системе");
                    }

                    context.Implementers.Add(CreateModel(model, tempImplementer));
                }

                context.SaveChanges();
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Implementers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                List<ImplementerViewModel> result = new List<ImplementerViewModel>();

                if (model != null)
                {
                    result.AddRange(context.Implementers.Where(rec => model.Id.HasValue && rec.Id == model.Id)
                        .Select(rec => CreateViewModel(rec)));
                }
                else
                {
                    result.AddRange(context.Implementers.Select(rec => CreateViewModel(rec)));
                }
                return result;
            }
        }

        private Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                implementer.ImplementerFIO = model.ImplementerFIO;
                implementer.WorkingTime = model.WorkingTime;
                implementer.PauseTime = model.PauseTime;

                return implementer;
            }
        }

        static private ImplementerViewModel CreateViewModel(Implementer implementer)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                return new ImplementerViewModel
                {
                    Id = implementer.Id,
                    ImplementerFIO = implementer.ImplementerFIO,
                    WorkingTime = implementer.WorkingTime,
                    PauseTime = implementer.PauseTime
                };
            }
        }
    }
}
