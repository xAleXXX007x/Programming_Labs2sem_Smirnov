using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private readonly IMessageInfoLogic _messageLogic;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 10;
        public ClientController(IClientLogic logic, IMessageInfoLogic messageLogic)
        {
            _logic = logic;
            _messageLogic = messageLogic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password) => _logic.Read(new ClientBindingModel
        { Email = login, Password = password })?[0];

        [HttpGet]
        public List<MessageInfoViewModel> GetMessages(int clientId) => _messageLogic.Read(new MessageInfoBindingModel { ClientId = clientId });

        [HttpPost]
        public void Register(ClientBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        [HttpPost]
        public void UpdateData(ClientBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        private void CheckData(ClientBindingModel model)
        {
            if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new Exception("В качестве логина почта указана должна быть");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength || !Regex.IsMatch(model.Password,
            @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен быть и из цифр, букв и небуквенных символов должен состоять");
            }
        }

    }
}