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
    public class ClientViewModel : BaseViewModel
    {
        [DataMember]
        [DisplayName("Клиент")]
        [Column(title: "Клиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Почта")]
        [Column(title: "E-Mail", width: 150)]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль")]
        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ClientFIO",
            "Email",
            "Password"
        };
    }
}
