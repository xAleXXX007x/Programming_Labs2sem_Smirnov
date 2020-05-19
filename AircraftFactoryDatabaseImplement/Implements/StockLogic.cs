using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Implements
{
    public class StockLogic : IStockLogic
    {
        public List<StockViewModel> GetList()
        {
            using (var context = new AircraftFactoryDatabase())
            {
                List<StockViewModel> result = context.Stocks.Select(rec => new StockViewModel
                {
                    Id = rec.Id,
                    StockName = rec.StockName,
                    StockParts = context.StockParts
                .Where(recAP => recAP.StockId == rec.Id)
                .Select(recAP => new StockPartViewModel
                {
                    Id = recAP.Id,
                    StockId = recAP.StockId,
                    PartId = recAP.PartId,
                    PartName = recAP.Part.PartName,
                    Count = recAP.Count
                })
                .ToList()
                })
                .ToList();
                return result;
            }
        }
        public StockViewModel GetElement(int id)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    return new StockViewModel
                    {
                        Id = element.Id,
                        StockName = element.StockName,
                        StockParts = context.StockParts
                    .Where(recAP => recAP.StockId == element.Id)
                    .Select(recAP => new StockPartViewModel
                    {
                        Id = recAP.Id,
                        StockId = recAP.StockId,
                        PartId = recAP.PartId,
                        PartName = recAP.Part.PartName,
                        Count = recAP.Count
                    })
                    .ToList()
                    };
                }
                throw new Exception("Элемент не найден");
            }
        }
        public void AddElement(StockBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName);
                        if (element != null)
                        {
                            throw new Exception("Уже есть самолет с таким названием");
                        }
                        element = new Stock
                        {
                            StockName = model.StockName
                        };
                        context.Stocks.Add(element);
                        context.SaveChanges();

                        var groupParts = model.StockParts
                        .GroupBy(rec => rec.PartId)
                        .Select(rec => new
                        {
                            PartId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });

                        foreach (var groupPart in groupParts)
                        {
                            context.StockParts.Add(new StockPart
                            {
                                StockId = element.Id,
                                PartId = groupPart.PartId,
                                Count = groupPart.Count
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void UpdElement(StockBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть самолет с таким названием");
                        }
                        element = context.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.StockName = model.StockName;
                        context.SaveChanges();
                        var compIds = model.StockParts.Select(rec =>
                        rec.PartId).Distinct();
                        var updateParts = context.StockParts.Where(rec => rec.StockId == model.Id && compIds.Contains(rec.PartId));
                        foreach (var updatePart in updateParts)
                        {
                            updatePart.Count = model.StockParts.FirstOrDefault(rec => rec.Id == updatePart.Id).Count;
                        }
                        context.SaveChanges();
                        context.StockParts.RemoveRange(context.StockParts.Where(rec =>
                        rec.StockId == model.Id && !compIds.Contains(rec.PartId)));
                        context.SaveChanges();

                        var groupParts = model.StockParts
                        .Where(rec => rec.Id == 0)
                        .GroupBy(rec => rec.PartId)
                        .Select(rec => new
                        {
                            PartId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                        foreach (var groupPart in groupParts)
                        {
                            StockPart elementAP = context.StockParts.FirstOrDefault(rec => rec.StockId == model.Id
                            && rec.PartId == groupPart.PartId);
                            if (elementAP != null)
                            {
                                if (groupPart.Count <= 0)
                                {
                                    context.StockParts.Remove(elementAP);
                                } else
                                {
                                    elementAP.Count += groupPart.Count;
                                }
                                context.SaveChanges();
                            }
                            else
                            {
                                context.StockParts.Add(new StockPart
                                {
                                    StockId = model.Id,
                                    PartId = groupPart.PartId,
                                    Count = groupPart.Count
                                });
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
                        if (element != null)
                        {
                            context.StockParts.RemoveRange(context.StockParts.Where(rec => rec.StockId == id));
                            context.Stocks.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void RefillStock(StockBindingModel model, StockPartBindingModel partModel)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
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
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void WithdrawStock(OrderViewModel order)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var aircraftParts = context.AircraftParts.Where(rec => rec.AircraftId == order.AircraftId);
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

                            if (partsLeft > 0)
                            {
                                throw new Exception("Недостаточно запчастей для выполнения заказа");
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
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }

}
