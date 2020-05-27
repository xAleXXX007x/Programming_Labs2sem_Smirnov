using AircraftFactoryBusinessLogic.Attributes;
using AircraftFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class ImplementerViewModel : BaseViewModel
    {
        [DataMember]
        [DisplayName("ФИО исполнителя")]
        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [DisplayName("Время на заказ")]
        [Column(title: "Время на заказ", width: 50)]
        public int WorkingTime { get; set; }

        [DataMember]
        [DisplayName("Время на перерыв")]
        [Column(title: "Время на перерыв", width: 50)]
        public int PauseTime { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ImplementerFIO",
            "WorkingTime",
            "PauseTime"
        };
    }
}
