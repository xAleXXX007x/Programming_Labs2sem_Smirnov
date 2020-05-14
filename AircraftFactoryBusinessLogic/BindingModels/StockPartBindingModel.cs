using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class StockPartBindingModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int PartId { get; set; }
        
        public int Count { get; set; }
    }
}
