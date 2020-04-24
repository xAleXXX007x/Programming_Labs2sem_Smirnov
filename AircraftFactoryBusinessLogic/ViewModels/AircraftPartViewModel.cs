using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class AircraftPartViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int AircraftId { get; set; }

        [DataMember]
        public int PartId { get; set; }

        [DataMember]
        [DisplayName("Запчасть")]
        public string PartName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
