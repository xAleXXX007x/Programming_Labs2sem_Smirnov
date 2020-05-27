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
    public class OrderViewModel : BaseViewModel
    {

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int AircraftId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        [DisplayName("Исполнитель")]
        [Column(title: "Исполнитель", width: 150)]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [DisplayName("Клиент")]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Самолет")]
        [Column(title: "Изделие", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string AircraftName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        [Column(title: "Количество", width: 50)]
        public int Count { get; set; }

        [DataMember]
        [DisplayName("Сумма")]
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Статус")]
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        [Column(title: "Дата создания", width: 100)]
        public DateTime DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата выполнения")]
        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ImplementerFIO",
            "ClientFIO",
            "AircraftName",
            "Count",
            "Sum",
            "Status",
            "DateCreate",
            "DateImplement"
        };
    }
}
