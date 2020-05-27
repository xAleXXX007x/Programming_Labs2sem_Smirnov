using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.BindingModels
{
    public class RefillStockBindingModel
    {
        public int StockId { get; set; }

        public int PartId { get; set; }

        public int Count { get; set; }
    }
}
