using AircraftFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AircraftFactoryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string PartFileName = "Part.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string AircraftFileName = "Aircraft.xml";

        private readonly string AircraftPartFileName = "AircraftPart.xml";

        private readonly string ClientFileName = "Client.xml";

        private readonly string ImplementerFileName = "Implementer.xml";

        public List<Part> Parts { get; set; }

        public List<Order> Orders { get; set; }

        public List<Aircraft> Aircrafts { get; set; }

        public List<AircraftPart> AircraftParts { get; set; }

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { get; set; }

        private FileDataListSingleton()
        {
            Parts = LoadParts();
            Orders = LoadOrders();
            Aircrafts = LoadAircrafts();
            AircraftParts = LoadAircraftParts();
            Clients = LoadClients();
            Implementers = LoadImplementers();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveParts();
            SaveOrders();
            SaveAircrafts();
            SaveAircraftParts();
            SaveClients();
            SaveImplementers();
        }

        private List<Part> LoadParts()
        {
            var list = new List<Part>();
            if (File.Exists(PartFileName))
            {
                XDocument xDocument = XDocument.Load(PartFileName);
                var xElements = xDocument.Root.Elements("Part").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Part
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PartName = elem.Element("PartName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        AircraftId = Convert.ToInt32(elem.Element("AircraftId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value)
                    });
                }
            }
            return list;
        }

        private List<Aircraft> LoadAircrafts()
        {
            var list = new List<Aircraft>();
            if (File.Exists(AircraftFileName))
            {
                XDocument xDocument = XDocument.Load(AircraftFileName);
                var xElements = xDocument.Root.Elements("Aircraft").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Aircraft
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        AircraftName = elem.Element("AircraftName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<AircraftPart> LoadAircraftParts()
        {
            var list = new List<AircraftPart>();
            if (File.Exists(AircraftPartFileName))
            {
                XDocument xDocument = XDocument.Load(AircraftPartFileName);
                var xElements = xDocument.Root.Elements("AircraftPart").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new AircraftPart
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        AircraftId = Convert.ToInt32(elem.Element("AircraftId").Value),
                        PartId = Convert.ToInt32(elem.Element("PartId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ClientFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("Email").Value),
                        PauseTime = Convert.ToInt32(elem.Element("Password").Value)
                    });
                }
            }
            return list;
        }

        private void SaveParts()
        {
            if (Parts != null)
            {
                var xElement = new XElement("Parts");
                foreach (var part in Parts)
                {
                    xElement.Add(new XElement("Part",
                    new XAttribute("Id", part.Id),
                    new XElement("PartName", part.PartName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(PartFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("AircraftId", order.AircraftId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveAircrafts()
        {
            if (Aircrafts != null)
            {
                var xElement = new XElement("Aircrafts");
                foreach (var aircraft in Aircrafts)
                {
                    xElement.Add(new XElement("Aircraft",
                    new XAttribute("Id", aircraft.Id),
                    new XElement("AircraftName", aircraft.AircraftName),
                    new XElement("Price", aircraft.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(AircraftFileName);
            }
        }
        private void SaveAircraftParts()
        {
            if (AircraftParts != null)
            {
                var xElement = new XElement("AircraftParts");
                foreach (var aircraftPart in AircraftParts)
                {
                    xElement.Add(new XElement("AircraftPart",
                    new XAttribute("Id", aircraftPart.Id),
                    new XElement("AircraftId", aircraftPart.AircraftId),
                    new XElement("PartId", aircraftPart.PartId),
                    new XElement("Count", aircraftPart.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(AircraftPartFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
    }
}
