using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Part> Parts { get; set; }

        public List<Order> Orders { get; set; }

        public List<Aircraft> Aircrafts { get; set; }

        public List<AircraftPart> AircraftParts { get; set; }

        public List<Stock> Stocks { get; set; }

        public List<StockPart> StockParts { get; set; }

        private DataListSingleton()
        {
            Parts = new List<Part>();
            Orders = new List<Order>();
            Aircrafts = new List<Aircraft>();
            AircraftParts = new List<AircraftPart>();
            Stocks = new List<Stock>();
            StockParts = new List<StockPart>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
