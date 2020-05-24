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

        public void RefillStock(StockBindingModel model, StockPartBindingModel partModel)
        {
            var stock = GetElement(model.Id);
            if (stock == null)
            {
                throw new Exception("Не найден склад");
            }

            foreach (var stockPart in stock.StockParts)
            {
                if (stockPart.PartId.Equals(partModel.PartId))
                {
                    var part = source.StockParts.FirstOrDefault(rec => rec.Id == stockPart.Id);

                    if (part != null)
                    {
                        part.Count += partModel.Count;
                        return;
                    }
                }
            }

            int maxSPId = source.StockParts.Count > 0 ? source.StockParts.Max(rec => rec.Id) : 0;

            source.StockParts.Add(new StockPart
            {
                Id = maxSPId,
                StockId = model.Id,
                PartId = partModel.PartId,
                Count = partModel.Count
            });
        }

        public void WithdrawStock(OrderViewModel order)
        {
            var aircraftParts = 
                source.AircraftParts
                .Where(rec => rec.AircraftId == order.AircraftId)
                .ToDictionary(rec => rec.PartId, rec => rec.Count * order.Count);
            var stockParts = 
                source.StockParts
                .Where(rec => aircraftParts.ContainsKey(rec.PartId))
                .GroupBy(rec => rec.PartId)
                .Select(rec => new
                {
                    PartId = rec.Key,
                    Count = rec.Sum(r => r.Count)
                });

            foreach(var stockPart in stockParts)
            {
                if (aircraftParts[stockPart.PartId] > stockPart.Count)
                {
                    throw new Exception("Недостаточно запчастей для выполнения заказа");
                }
            }

            foreach (var aircraftPart in aircraftParts)
            {
                int partsLeft = aircraftPart.Value;
                var stockPartList = source.StockParts.Where(rec => rec.PartId == aircraftPart.Key).ToList();

                foreach (var stockPart in stockPartList)
                {
                    if (stockPart.Count > partsLeft)
                    {
                        stockPart.Count -= partsLeft;
                        partsLeft = 0;

                        break;
                    } else
                    {
                        partsLeft -= stockPart.Count;
                        source.StockParts.Remove(stockPart);
                    }
                }
            }
        }
    }

}
