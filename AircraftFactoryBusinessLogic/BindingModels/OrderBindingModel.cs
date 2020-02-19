using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public int AircraftId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
