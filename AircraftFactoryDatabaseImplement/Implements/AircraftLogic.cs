using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryDatabaseImplement.Implements
{
    public class AircraftLogic : IAircraftLogic
    {
        public List<AircraftViewModel> GetList()
        {
            using (var context = new AircraftFactoryDatabase())
            {
                List<AircraftViewModel> result = context.Aircrafts.Select(rec => new AircraftViewModel
                {
                    Id = rec.Id,
                    AircraftName = rec.AircraftName,
                    Price = rec.Price,
                    AircraftParts = context.AircraftParts
                .Where(recAP => recAP.AircraftId == rec.Id)
                .Select(recAP => new AircraftPartViewModel
                {
                    Id = recAP.Id,
                    AircraftId = recAP.AircraftId,
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
        public AircraftViewModel GetElement(int id)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                Aircraft element = context.Aircrafts.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    return new AircraftViewModel
                    {
                        Id = element.Id,
                        AircraftName = element.AircraftName,
                        Price = element.Price,
                        AircraftParts = context.AircraftParts
                    .Where(recAP => recAP.AircraftId == element.Id)
                    .Select(recAP => new AircraftPartViewModel
                    {
                        Id = recAP.Id,
                        AircraftId = recAP.AircraftId,
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
        public void AddElement(AircraftBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Aircraft element = context.Aircrafts.FirstOrDefault(rec => rec.AircraftName == model.AircraftName);
                        if (element != null)
                        {
                            throw new Exception("Уже есть самолет с таким названием");
                        }
                        element = new Aircraft
                        {
                            AircraftName = model.AircraftName,
                            Price = model.Price
                        };
                        context.Aircrafts.Add(element);
                        context.SaveChanges();

                        var groupParts = model.AircraftParts
                        .GroupBy(rec => rec.PartId)
                        .Select(rec => new
                        {
                            PartId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });

                        foreach (var groupPart in groupParts)
                        {
                            context.AircraftParts.Add(new AircraftPart
                            {
                                AircraftId = element.Id,
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
        public void UpdElement(AircraftBindingModel model)
        {
            using (var context = new AircraftFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Aircraft element = context.Aircrafts.FirstOrDefault(rec => rec.AircraftName == model.AircraftName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть самолет с таким названием");
                        }
                        element = context.Aircrafts.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.AircraftName = model.AircraftName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        var compIds = model.AircraftParts.Select(rec =>
                        rec.PartId).Distinct();
                        var updateParts = context.AircraftParts.Where(rec => rec.AircraftId == model.Id && compIds.Contains(rec.PartId));
                        foreach (var updatePart in updateParts)
                        {
                            updatePart.Count = model.AircraftParts.FirstOrDefault(rec => rec.Id == updatePart.Id).Count;
                        }
                        context.SaveChanges();
                        context.AircraftParts.RemoveRange(context.AircraftParts.Where(rec =>
                        rec.AircraftId == model.Id && !compIds.Contains(rec.PartId)));
                        context.SaveChanges();

                        var groupParts = model.AircraftParts
                        .Where(rec => rec.Id == 0)
                        .GroupBy(rec => rec.PartId)
                        .Select(rec => new
                        {
                            PartId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                        foreach (var groupPart in groupParts)
                        {
                            AircraftPart elementAP = context.AircraftParts.FirstOrDefault(rec => rec.AircraftId == model.Id
                            && rec.PartId == groupPart.PartId);
                            if (elementAP != null)
                            {
                                elementAP.Count += groupPart.Count;
                                context.SaveChanges();
                            }
                            else
                            {
                                context.AircraftParts.Add(new AircraftPart
                                {
                                    AircraftId = model.Id,
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
                        Aircraft element = context.Aircrafts.FirstOrDefault(rec => rec.Id == id);
                        if (element != null)
                        {
                            context.AircraftParts.RemoveRange(context.AircraftParts.Where(rec => rec.AircraftId == id));
                            context.Aircrafts.Remove(element);
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
    }

}
