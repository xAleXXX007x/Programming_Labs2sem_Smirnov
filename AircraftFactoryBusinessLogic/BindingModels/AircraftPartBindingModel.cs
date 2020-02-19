using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class AircraftPartBindingModel
    {
        public int Id { get; set; }

        public int AircraftId { get; set; }

        public int PartId { get; set; }
        
        public int Count { get; set; }
    }
}
