using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportAircraftsViewModel> Aircrafts { get; set; }

        public List<ReportStockPartViewModel> StockParts { get; set; }
    }
}
