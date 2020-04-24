﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftFactoryRestApi.Models
{
    public class AircraftModel
    {
        public int Id { get; set; }

        public string AircraftName { get; set; }

        public decimal Price { get; set; } 
    }
}
