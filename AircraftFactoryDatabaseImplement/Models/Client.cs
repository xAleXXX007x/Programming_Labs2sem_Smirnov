using AircraftFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientFIO { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual List<Order> Orders { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<MessageInfo> MessageInfoes { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { get; set; }
    }
}
