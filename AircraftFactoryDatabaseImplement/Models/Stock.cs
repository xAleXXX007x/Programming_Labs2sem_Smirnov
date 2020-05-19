using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public string StockName { get; set; }

        [ForeignKey("StockId")]
        public virtual List<StockPart> StockParts { get; set; }
    }
}
