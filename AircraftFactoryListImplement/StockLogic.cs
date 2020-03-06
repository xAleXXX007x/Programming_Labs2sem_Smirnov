using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryListImplement
{
    public class StockLogic : IStockLogic
    {
        private readonly DataListSingleton source;

        public StockLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = new List<StockViewModel>();
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                List<StockPartViewModel> stockParts = new List<StockPartViewModel>();

                for (int j = 0; j < source.StockParts.Count; ++j)
                {
                    if (source.StockParts[j].StockId == source.Stocks[i].Id)
                    {
                        string partName = string.Empty;

                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.StockParts[j].PartId == source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;

                                break;
                            }
                        }

                        stockParts.Add(new StockPartViewModel
                        {
                            Id = source.StockParts[j].Id,
                            StockId = source.StockParts[j].StockId,
                            PartId = source.StockParts[j].PartId,
                            PartName = partName,
                            Count = source.StockParts[j].Count
                        });
                    }
                }
                result.Add(new StockViewModel
                {
                    Id = source.Stocks[i].Id,
                    StockName = source.Stocks[i].StockName,
                    StockParts = stockParts
                });
            }

            return result;
        }

        public StockViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                List<StockPartViewModel> stockParts = new List<StockPartViewModel>();

                for (int j = 0; j < source.StockParts.Count; ++j)
                {
                    if (source.StockParts[j].StockId == source.Stocks[i].Id)
                    {
                        string partName = string.Empty;

                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.StockParts[j].PartId == source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;

                                break;
                            }
                        }

                        stockParts.Add(new StockPartViewModel
                        {
                            Id = source.StockParts[j].Id,
                            StockId = source.StockParts[j].StockId,
                            PartId = source.StockParts[j].PartId,
                            PartName = partName,
                            Count = source.StockParts[j].Count
                        });
                    }
                }

                if (source.Stocks[i].Id == id)
                {
                    return new StockViewModel
                    {
                        Id = source.Stocks[i].Id,
                        StockName = source.Stocks[i].StockName,
                        StockParts = stockParts
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockBindingModel model)
        {
            int maxId = 0;

            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                if (source.Stocks[i].Id > maxId)
                {
                    maxId = source.Stocks[i].Id;
                }

                if (source.Stocks[i].StockName == model.StockName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }

            source.Stocks.Add(new Stock
            {
                Id = maxId + 1,
                StockName = model.StockName
            });
        }
        public void UpdElement(StockBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                if (source.Stocks[i].Id == model.Id)
                {
                    index = i;
                }

                if (source.Stocks[i].StockName == model.StockName &&
                source.Stocks[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            source.Stocks[index].StockName = model.StockName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.StockParts.Count; ++i)
            {
                if (source.StockParts[i].StockId == id)
                {
                    source.StockParts.RemoveAt(i--);
                }
            }

            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                if (source.Stocks[i].Id == id)
                {
                    source.Stocks.RemoveAt(i);

                    return;
                }
            }

            throw new Exception("Элемент не найден");
        }
    }

}
