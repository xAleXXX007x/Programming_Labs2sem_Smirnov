using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    public class StockPartViewModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int PartId { get; set; }

        [DisplayName("Запчасть")]
        public string PartName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
