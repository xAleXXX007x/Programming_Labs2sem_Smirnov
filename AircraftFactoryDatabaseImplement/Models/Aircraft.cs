using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        [Required]
        public string AircraftName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual AircraftPart AircraftPart { get; set; }

        [ForeignKey("AircraftId")]
        public virtual List<Order> Orders { get; set; }
    }
}
