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
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        [DisplayName("Отправитель")]
        [Column(title: "Отправитель", width: 100)]
        public string SenderName { get; set; }

        [DataMember]
        [DisplayName("Дата письма")]
        [Column(title: "Дата письма", width: 100)]
        public DateTime DateDelivery { get; set; }

        [DataMember]
        [DisplayName("Заголовок")]
        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }

        [DataMember]
        [DisplayName("Текст")]
        [Column(title: "Текст", width: 150)]
        public string Body { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "SenderName",
            "DateDelivery",
            "Subject",
            "Body"
        };
    }
}
