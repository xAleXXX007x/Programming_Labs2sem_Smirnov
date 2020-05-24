using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required]
        public string PartName { get; set; }

        [ForeignKey("PartId")]
        public virtual List<AircraftPart> AircraftParts { get; set; }

        [ForeignKey("PartId")]
        public virtual List<StockPart> StockParts { get; set; }
    }
}
