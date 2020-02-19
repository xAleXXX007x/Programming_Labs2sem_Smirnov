using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryListImplement
{
    public class AircraftPart
    {
        public int Id { get; set; }

        public int AircraftId { get; set; }

        public int PartId { get; set; }

        public int Count { get; set; }
    }
}
