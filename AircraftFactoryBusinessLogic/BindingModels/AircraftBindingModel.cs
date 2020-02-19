using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class AircraftBindingModel
    {
        public int Id { get; set; }

        public string AircraftName { get; set; }

        public decimal Price { get; set; }

        public List<AircraftPartBindingModel> AircraftParts { get; set; }
    }
}
