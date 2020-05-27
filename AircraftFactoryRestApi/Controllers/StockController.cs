using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryDatabaseImplement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AircraftFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockLogic _logic;

        private readonly IPartLogic _partLogic;

        public StockController(IStockLogic logic, IPartLogic partLogic)
        {
            _logic = logic;
            _partLogic = partLogic;
        }

        [HttpGet]
        public List<StockViewModel> GetStocks() => _logic.GetList();

        [HttpGet]
        public StockViewModel GetStock(int id) => _logic.GetElement(id);

        [HttpGet]
        public List<PartViewModel> GetParts() => _partLogic.GetList();

        [HttpPost]
        public void CreateStock(StockBindingModel model) => _logic.AddElement(model);

        [HttpPost]
        public void UpdateStock(StockBindingModel model) => _logic.UpdElement(model);

        [HttpPost]
        public void DeleteStock(StockBindingModel model) => _logic.DelElement(model.Id);

        [HttpPost]
        public void RefillStock(RefillStockBindingModel model) => _logic.RefillStock(new StockBindingModel { Id = model.StockId }, new StockPartBindingModel { PartId = model.PartId, Count = model.Count });
    }
}