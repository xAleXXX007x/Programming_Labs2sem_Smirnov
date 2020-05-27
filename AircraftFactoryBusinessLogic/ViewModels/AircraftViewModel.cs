using AircraftFactoryBusinessLogic.Attributes;
using AircraftFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class AircraftViewModel : BaseViewModel
    {
        [DataMember]
        [DisplayName("Название самолета")]
        [Column(title: "Название самолета", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string AircraftName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }

        [DataMember]
        public List<AircraftPartViewModel> AircraftParts { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "AircraftName",
            "Price"
        };
    }
}
