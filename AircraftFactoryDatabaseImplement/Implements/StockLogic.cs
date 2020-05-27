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
                            throw new Exception("Уже есть склад с таким названием");
                        }
                        element = new Stock
                        {
                            StockName = model.StockName
                        };
                        context.Stocks.Add(element);
                        context.SaveChanges();

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

                        foreach (var stockPart in stock.StockParts)
                        {
                            if (stockPart.PartId.Equals(partModel.PartId))
                            {
                                var part = context.StockParts.FirstOrDefault(rec => rec.Id == stockPart.Id);

                                if (part != null)
                                {
                                    part.Count += partModel.Count;
                                    context.SaveChanges();
                                    transaction.Commit();
                                    return;
                                }
                            }
                        }

                        context.StockParts.Add(new StockPart
                        {
                            StockId = model.Id,
                            PartId = partModel.PartId,
                            Count = partModel.Count
                        });
                        context.SaveChanges();
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
                        var aircraftParts =
                            context.AircraftParts
                            .Where(rec => rec.AircraftId == order.AircraftId)
                            .ToDictionary(rec => rec.PartId, rec => rec.Count * order.Count);

                        foreach (var aircraftPart in aircraftParts)
                        {
                            int partsLeft = aircraftPart.Value;
                            var stockPartList = context.StockParts.Where(rec => rec.PartId == aircraftPart.Key).ToList();

                            foreach (var stockPart in stockPartList)
                            {
                                if (stockPart.Count > partsLeft)
                                {
                                    stockPart.Count -= partsLeft;
                                    partsLeft = 0;
                                    context.SaveChanges();

                                    break;
                                }
                                else
                                {
                                    partsLeft -= stockPart.Count;
                                    context.StockParts.Remove(stockPart);
                                    context.SaveChanges();
                                }
                            }

                            if (partsLeft > 0)
                            {
                                throw new Exception("Недостаточно запчастей для выполнения заказа");
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
    }

}
