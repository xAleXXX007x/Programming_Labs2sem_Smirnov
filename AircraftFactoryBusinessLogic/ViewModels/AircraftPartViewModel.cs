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
    public class AircraftPartViewModel : BaseViewModel
    {
        [DataMember]
        public int AircraftId { get; set; }

        [DataMember]
        public int PartId { get; set; }

        [DataMember]
        [DisplayName("Запчасть")]
        [Column(title: "Запчасть", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string PartName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        [Column(title: "Сумма", width: 50)]
        public int Count { get; set; }

        public override List<string> Properties() => new List<string> {
            "Id",
            "PartName",
            "Count"
        };
    }
}
