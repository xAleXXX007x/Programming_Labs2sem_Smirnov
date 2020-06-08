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

            int maxAId = 0;

            for (int i = 0; i < source.StockParts.Count; ++i)
            {
                if (source.StockParts[i].Id > maxAId)
                {
                    maxAId = source.StockParts[i].Id;
                }
            }

            for (int i = 0; i < model.StockParts.Count; ++i)
            {
                for (int j = 1; j < model.StockParts.Count; ++j)
                {
                    if (model.StockParts[i].PartId == model.StockParts[j].PartId)
                    {
                        model.StockParts[i].Count +=
                        model.StockParts[j].Count;
                        model.StockParts.RemoveAt(j--);
                    }
                }
            }

            for (int i = 0; i < model.StockParts.Count; ++i)
            {
                source.StockParts.Add(new StockPart
                {
                    Id = ++maxAId,
                    StockId = maxId + 1,
                    PartId = model.StockParts[i].PartId,
                    Count = model.StockParts[i].Count
                });
            }
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

            UpdateStockParts(model);
        }

        private void UpdateStockParts(StockBindingModel model)
        {
            int maxAId = 0;

            for (int i = 0; i < source.StockParts.Count; ++i)
            {
                if (source.StockParts[i].Id > maxAId)
                {
                    maxAId = source.StockParts[i].Id;
                }
            }

            for (int i = 0; i < source.StockParts.Count; ++i)
            {
                if (source.StockParts[i].StockId == model.Id)
                {
                    bool flag = true;

                    for (int j = 0; j < model.StockParts.Count; ++j)
                    {
                        if (source.StockParts[i].Id ==
                        model.StockParts[j].Id)
                        {
                            source.StockParts[i].Count =
                            model.StockParts[j].Count;
                            flag = false;

                            break;
                        }
                    }

                    if (flag)
                    {
                        source.StockParts.RemoveAt(i--);
                    }
                }
            }

            for (int i = 0; i < model.StockParts.Count; ++i)
            {
                if (model.StockParts[i].Id == 0)
                {
                    for (int j = 0; j < source.StockParts.Count; ++j)
                    {
                        if (source.StockParts[j].StockId == model.Id &&
                        source.StockParts[j].PartId == model.StockParts[i].PartId)
                        {
                            source.StockParts[j].Count +=
                            model.StockParts[i].Count;
                            model.StockParts[i].Id = source.StockParts[j].Id;

                            break;
                        }
                    }

                    if (model.StockParts[i].Id == 0)
                    {
                        source.StockParts.Add(new StockPart
                        {
                            Id = ++maxAId,
                            StockId = model.Id,
                            PartId = model.StockParts[i].PartId,
                            Count = model.StockParts[i].Count
                        });
                    }
                }
            }
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

        public void RefillStock(StockBindingModel model, StockPartBindingModel partModel)
        {
            var stock = GetElement(model.Id);
            if (stock == null)
            {
                throw new Exception("Не найден склад");
            }

            List<StockPartBindingModel> stockParts = model.StockParts;
            bool found = false;
            foreach (StockPartBindingModel stockPart in stockParts)
            {
                if (stockPart.PartId == partModel.PartId)
                {
                    stockPart.Count += partModel.Count;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                stockParts.Add(partModel);
            }

            UpdElement(new StockBindingModel
            {
                Id = stock.Id,
                StockName = stock.StockName,
                StockParts = stockParts
            });
        }

        public bool WithdrawStock(OrderViewModel order)
        {
            var aircraftParts = new List<AircraftPart>();

            foreach (var part in source.AircraftParts)
            {
                if (part.AircraftId == order.AircraftId)
                {
                    aircraftParts.Add(part);
                }
            }

            var stocks = GetList();

            foreach (var part in aircraftParts)
            {
                int count = 0;

                foreach (var stock in stocks)
                {
                    foreach (var stockPart in stock.StockParts)
                    {
                        if (stockPart.PartId.Equals(part.PartId))
                        {
                            count += stockPart.Count;
                        }
                    }
                }

                if (count < part.Count * order.Count)
                {
                    return false;
                }
                else
                {
                    int partsLeft = part.Count * order.Count;

                    foreach (var stock in stocks)
                    {
                        List<StockPartViewModel> delete = new List<StockPartViewModel>();

                        foreach (var stockPart in stock.StockParts)
                        {
                            if (partsLeft > 0)
                            {
                                if (stockPart.PartId.Equals(part.PartId))
                                {
                                    if (stockPart.Count <= partsLeft)
                                    {
                                        partsLeft -= stockPart.Count;

                                        stockPart.Count = 0;

                                        if (partsLeft == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        stockPart.Count -= partsLeft;
                                        partsLeft = 0;

                                        continue;
                                    }
                                }
                            }
                        }

                        if (partsLeft == 0)
                        {
                            continue;
                        }
                    }
                }
            }

            foreach (var stock in stocks)
            {
                List<StockPartBindingModel> parts = new List<StockPartBindingModel>();

                foreach (var part in stock.StockParts)
                {
                    parts.Add(new StockPartBindingModel
                    {
                        Id = part.Id,
                        StockId = part.StockId,
                        PartId = part.PartId,
                        Count = part.Count
                    });
                }

                UpdElement(new StockBindingModel
                {
                    Id = stock.Id,
                    StockName = stock.StockName,
                    StockParts = parts
                });
            }

            return true;
        }
    }

}
