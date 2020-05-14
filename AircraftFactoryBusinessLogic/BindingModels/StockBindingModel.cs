using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class StockBindingModel
    {
        public int Id { get; set; }

        public string StockName { get; set; }

        public List<StockPartBindingModel> StockParts { get; set; }
    }
}
