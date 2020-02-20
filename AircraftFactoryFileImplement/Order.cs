using AircraftFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryFileImplement
{
    public class Order
    {
        public int Id { get; set; }

        public int AircraftId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}
