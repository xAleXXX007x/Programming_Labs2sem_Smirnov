using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    public class AircraftViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название самолета")]
        public string AircraftName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public List<AircraftPartViewModel> AircraftParts { get; set; }
    }
}
