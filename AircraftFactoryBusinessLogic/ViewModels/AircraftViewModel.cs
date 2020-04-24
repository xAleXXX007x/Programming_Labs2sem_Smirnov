using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class AircraftViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название самолета")]
        public string AircraftName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        public List<AircraftPartViewModel> AircraftParts { get; set; }
    }
}
