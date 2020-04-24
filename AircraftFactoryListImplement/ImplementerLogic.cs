using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryListImplement
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly DataListSingleton source;

        public ImplementerLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            Implementer tempImplementer = model.Id.HasValue ? null : new Implementer { Id = 1 };
            foreach (var implementer in source.Implementers)
            {
                if (!model.Id.HasValue && implementer.Id >= tempImplementer.Id)
                {
                    tempImplementer.Id = implementer.Id + 1;
                }
                else if (model.Id.HasValue && implementer.Id == model.Id)
                {
                    tempImplementer = implementer;
                }
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
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id.Value)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var implementer in source.Implementers)
            {
                if (model != null)
                {
                    if (implementer.Id == model.Id)
                    {
                        result.Add(CreateViewModel(implementer));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(implementer));
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
