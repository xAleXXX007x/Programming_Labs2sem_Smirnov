using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string StockName { get; set; }

        public List<StockPartViewModel> StockParts { get; set; }
    }
}
