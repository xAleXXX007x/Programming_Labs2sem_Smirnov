using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    public class PartViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название запчасти")]
        public string PartName { get; set; }
    }
}
