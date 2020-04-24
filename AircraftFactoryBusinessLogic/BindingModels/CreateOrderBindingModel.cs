using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int AircraftId { get; set; }

        public int ClientId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
