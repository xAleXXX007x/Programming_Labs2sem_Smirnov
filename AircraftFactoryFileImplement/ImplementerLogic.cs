using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryFileImplement
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly FileDataListSingleton source;

        public ImplementerLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            Implementer tempImplementer = model.Id.HasValue ? null : new Implementer { Id = 1 };

            if (!model.Id.HasValue)
            {
                tempImplementer.Id = source.Implementers.FirstOrDefault(rec => rec.Id >= tempImplementer.Id).Id + 1;
            }
            else
            {
                tempImplementer = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
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
                source.Implementers.Add(CreateModel(model, tempImplementer));
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id.Value);

            if (element != null)
            {
                source.Implementers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();

            if (model != null)
            {
                result.Add(CreateViewModel(source.Implementers.FirstOrDefault(rec => rec.Id == model.Id)));
            }
            else
            {
                result.AddRange(source.Implementers.Select(rec => CreateViewModel(rec)));
            }
            return result;
        }

        private Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.WorkingTime = model.WorkingTime;
            implementer.PauseTime = model.PauseTime;

            return implementer;
        }

        private ImplementerViewModel CreateViewModel(Implementer implementer)
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
