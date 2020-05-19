using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Models
{
    public class StockPart
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int PartId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Part Part { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
