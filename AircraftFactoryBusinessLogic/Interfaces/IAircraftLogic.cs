using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.Interfaces
{
    public interface IAircraftLogic
    {
        List<AircraftViewModel> GetList();

        AircraftViewModel GetElement(int id);

        void AddElement(AircraftBindingModel model);

        void UpdElement(AircraftBindingModel model);

        void DelElement(int id);
    }
}
