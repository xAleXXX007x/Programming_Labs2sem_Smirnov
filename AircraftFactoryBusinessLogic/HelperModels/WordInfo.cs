using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<AircraftViewModel> Aircrafts { get; set; }

        public List<StockViewModel> Stocks { get; set; }
    }
}
