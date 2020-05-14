using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.Interfaces
{
    public interface IStockLogic
    {
        List<StockViewModel> GetList();

        StockViewModel GetElement(int id);

        void AddElement(StockBindingModel model);

        void UpdElement(StockBindingModel model);

        void DelElement(int id);
    }
}
