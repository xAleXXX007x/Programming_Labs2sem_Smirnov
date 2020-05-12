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
        private readonly IPartLogic partLogic;
        private readonly IAircraftLogic aircraftLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IAircraftLogic aircraftLogic, IPartLogic partLogic, IOrderLogic orderLLogic)
        {
            this.aircraftLogic = aircraftLogic;
            this.partLogic = partLogic;
            this.orderLogic = orderLLogic;
        }

        public List<ReportAircraftPartViewModel> GetAircraftPart()
        {
            var parts = partLogic.GetList();
            var aircrafts = aircraftLogic.GetList();
            var list = new List<ReportAircraftPartViewModel>();
            foreach (var part in parts)
            {
                var record = new ReportAircraftPartViewModel
                {
                    PartName = part.PartName,
                    Aircrafts = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var aircraft in aircrafts)
                {
                    bool contains = false;
                    int id = -1;

                    for (int i = 0; i < aircraft.AircraftParts.Count; i++)
                    {
                        var ap = aircraft.AircraftParts[i];

                        if (ap.PartId.Equals(part.Id))
                        {
                            contains = true;
                            id = i;
                            break;
                        }
                    }

                    if (contains)
                    {
                        var p = aircraft.AircraftParts[id];
                        record.Aircrafts.Add(new Tuple<string, int>(aircraft.AircraftName, p.Count));
                        record.TotalCount += p.Count;
                    }
                }
                list.Add(record);
            }
            return list;
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

        public void SaveAircraftsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список самолётов",
                Aircrafts = aircraftLogic.GetList()
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

        public void SaveAircraftsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список самолётов и их запчастей",
                Aircrafts = GetAircrafts()
            });
        }

    }
}
