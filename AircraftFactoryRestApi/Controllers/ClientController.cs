using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AircraftFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _logic;

        public ClientController(IClientLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password) => _logic.Read(new ClientBindingModel
        { Email = login, Password = password }).First();

        [HttpPost]
        public void Register(ClientBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(ClientBindingModel model) => _logic.CreateOrUpdate(model);
    }
}