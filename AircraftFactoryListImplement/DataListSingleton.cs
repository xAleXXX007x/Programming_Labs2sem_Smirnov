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

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { get; set; }

        public List<MessageInfo> MessageInfoes { get; set; }

        private DataListSingleton()
        {
            Parts = new List<Part>();
            Orders = new List<Order>();
            Aircrafts = new List<Aircraft>();
            AircraftParts = new List<AircraftPart>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
            MessageInfoes = new List<MessageInfo>();
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
