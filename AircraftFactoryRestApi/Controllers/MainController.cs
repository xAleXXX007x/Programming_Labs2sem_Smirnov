using AircraftFactoryBusinessLogic;
using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;

        private readonly IAircraftLogic _aircraft;

        private readonly MainLogic _main;

        public MainController(IOrderLogic order, IAircraftLogic aircraft, MainLogic main)
        {
            _order = order;
            _aircraft = aircraft;
            _main = main;
        }

        [HttpGet]
        public List<AircraftModel> GetAircraftList() 
        {
            List<AircraftModel> list = new List<AircraftModel>();

            foreach (var aircraft in _aircraft.GetList())
            {
                list.Add(Convert(aircraft));
            }

            return list;
        }

        [HttpGet]
        public AircraftModel GetAircraft(int aircraftId) => Convert(_aircraft.GetElement(aircraftId));
        
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        
        [HttpPost]
        public void CreateOrder(OrderBindingModel model) => _main.CreateOrder(model);
        
        private AircraftModel Convert(AircraftViewModel model)
        {
            if (model == null) return null;
            return new AircraftModel
            {
                Id = model.Id.Value,
                AircraftName = model.AircraftName,
                Price = model.Price
            };
        }
    }

}
