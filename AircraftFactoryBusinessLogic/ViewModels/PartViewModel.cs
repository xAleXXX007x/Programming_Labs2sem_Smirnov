using AircraftFactoryBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class PartViewModel : BaseViewModel
    {
        [DataMember]
        [DisplayName("Название запчасти")]
        [Column(title: "Название запчасти", width: 150)]
        public string PartName { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "PartName"
        };
    }
}
