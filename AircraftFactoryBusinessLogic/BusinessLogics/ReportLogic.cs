using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Enums;
using AircraftFactoryBusinessLogic.HelperModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IAircraftLogic aircraftLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IStockLogic stockLogic;
        public ReportLogic(IAircraftLogic aircraftLogic, IOrderLogic orderLogic, IStockLogic stockLogic)
        {
            this.aircraftLogic = aircraftLogic;
            this.orderLogic = orderLogic;
            this.stockLogic = stockLogic;
        }

        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(rec => rec.Key)
            .ToList();
        }

        public List<ReportAircraftsViewModel> GetAircrafts()
        {
            var list = new List<ReportAircraftsViewModel>();
            var aircrafts = aircraftLogic.GetList();

            foreach (var aircraft in aircrafts)
            {
                foreach (var part in aircraft.AircraftParts)
                {
                    var aircraftRecord = new ReportAircraftsViewModel
                    {
                        AircraftName = aircraft.AircraftName,
                        PartName = part.PartName,
                        Count = part.Count
                    };

                    list.Add(aircraftRecord);
                }
            }

            return list;
        }

        public List<ReportStockPartViewModel> GetStockParts()
        {
            var list = new List<ReportStockPartViewModel>();
            var stocks = stockLogic.GetList();

            foreach (var stock in stocks)
            {
                foreach (var part in stock.StockParts)
                {
                    var stockPartRecord = new ReportStockPartViewModel
                    {
                        StockName = stock.StockName,
                        PartName = part.PartName,
                        Count = part.Count
                    };

                    list.Add(stockPartRecord);
                }
            }

            return list;
        }

        public void SaveAircraftsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список самолётов",
                Aircrafts = aircraftLogic.GetList()
            });
        }

        public void SaveStocksToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Stocks = stockLogic.GetList()
            });
        }

        public void SaveOrdersToExcel(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        public void SaveStockPartsToExcel(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Stocks = stockLogic.GetList()
            });
        }

        public void SaveAircraftsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список самолётов и их запчастей",
                Aircrafts = GetAircrafts()
            });
        }

        public void SaveStockPartsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список запчастей на складах",
                StockParts = GetStockParts()
            });
        }
    }
}
