using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryFileImplement
{
    public class StockLogic : IStockLogic
    {
        private readonly FileDataListSingleton source;

        public StockLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = source.Stocks
            .Select(rec => new StockViewModel
            {
                Id = rec.Id,
                StockName = rec.StockName,
                StockParts = source.StockParts
            .Where(recPC => recPC.StockId == rec.Id)
            .Select(recPC => new StockPartViewModel
            {
                Id = recPC.Id,
                StockId = recPC.StockId,
                PartId = recPC.PartId,
                PartName = source.Parts.FirstOrDefault(recP => recP.Id == recPC.PartId)?.PartName,
                Count = recPC.Count
            })
            .ToList()
            })
            .ToList();
            return result;
        }

        public StockViewModel GetElement(int id)
        {
            Stock element = source.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StockViewModel
                {
                    Id = element.Id,
                    StockName = element.StockName,
                    StockParts = source.StockParts
                .Where(recPC => recPC.StockId == element.Id)
                .Select(recPC => new StockPartViewModel
                {
                    Id = recPC.Id,
                    StockId = recPC.StockId,
                    PartId = recPC.PartId,
                    PartName = source.Parts.FirstOrDefault(recP => recP.Id == recPC.PartId)?.PartName,
                    Count = recPC.Count
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockBindingModel model)
        {
            Stock element = source.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Stocks.Count > 0 ? source.Stocks.Max(rec => rec.Id) : 0;
            source.Stocks.Add(new Stock
            {
                Id = maxId + 1,
                StockName = model.StockName
            });
        }

        public void UpdElement(StockBindingModel model)
        {
            Stock element = source.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StockName = model.StockName;

            int maxSPId = source.StockParts.Count > 0 ? source.StockParts.Max(rec => rec.Id) : 0;
            foreach (var part in model.StockParts)
            {
                var stockPart = source.StockParts.FirstOrDefault(rec => rec.StockId == model.Id && rec.PartId == part.PartId);

                if (stockPart != null)
                {
                    stockPart.Count = part.Count;
                } else
                {
                    source.StockParts.Add(new StockPart
                    {
                        Id = ++maxSPId,
                        StockId = model.Id,
                        PartId = part.PartId,
                        Count = part.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Stock element = source.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.StockParts.RemoveAll(rec => rec.StockId == id);
                source.Stocks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }

}
