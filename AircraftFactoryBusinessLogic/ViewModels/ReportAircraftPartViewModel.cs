using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    public class ReportAircraftPartViewModel
    {
        public string PartName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Aircrafts { get; set; }

    }
}
