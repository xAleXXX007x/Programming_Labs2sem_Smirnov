using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftFactoryFileImplement
{
    public class AircraftLogic : IAircraftLogic
    {
        private readonly FileDataListSingleton source;

        public AircraftLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<AircraftViewModel> GetList()
        {
            List<AircraftViewModel> result = source.Aircrafts
            .Select(rec => new AircraftViewModel
            {
                Id = rec.Id,
                AircraftName = rec.AircraftName,
                Price = rec.Price,
                AircraftParts = source.AircraftParts
            .Where(recPC => recPC.AircraftId == rec.Id)
            .Select(recPC => new AircraftPartViewModel
            {
                Id = recPC.Id,
                AircraftId = recPC.AircraftId,
                PartId = recPC.PartId,
                PartName = source.Parts.FirstOrDefault(recP => recP.Id == recPC.PartId)?.PartName,
                Count = recPC.Count
            })
            .ToList()
            })
            .ToList();
            return result;
        }

        public AircraftViewModel GetElement(int id)
        {
            Aircraft element = source.Aircrafts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new AircraftViewModel
                {
                    Id = element.Id,
                    AircraftName = element.AircraftName,
                    Price = element.Price,
                    AircraftParts = source.AircraftParts
                .Where(recPC => recPC.AircraftId == element.Id)
                .Select(recPC => new AircraftPartViewModel
                {
                    Id = recPC.Id,
                    AircraftId = recPC.AircraftId,
                    PartId = recPC.PartId,
                    PartName = source.Parts.FirstOrDefault(recP => recP.Id == recPC.PartId)?.PartName,
                    Count = recPC.Count
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(AircraftBindingModel model)
        {
            Aircraft element = source.Aircrafts.FirstOrDefault(rec => rec.AircraftName == model.AircraftName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Aircrafts.Count > 0 ? source.Aircrafts.Max(rec => rec.Id) : 0;
            source.Aircrafts.Add(new Aircraft
            {
                Id = maxId + 1,
                AircraftName = model.AircraftName,
                Price = model.Price
            });

            int maxAPId = source.AircraftParts.Count > 0 ? source.AircraftParts.Max(rec => rec.Id) : 0;

            var groupParts = model.AircraftParts
            .GroupBy(rec => rec.PartId)
            .Select(rec => new
            {
                PartId = rec.Key,
                Count = rec.Sum(r => r.Count)
            });

            foreach (var groupPart in groupParts)
            {
                source.AircraftParts.Add(new AircraftPart
                {
                    Id = ++maxAPId,
                    AircraftId = maxId + 1,
                    PartId = groupPart.PartId,
                    Count = groupPart.Count
                });
            }
        }

        public void UpdElement(AircraftBindingModel model)
        {
            Aircraft element = source.Aircrafts.FirstOrDefault(rec => rec.AircraftName == model.AircraftName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Aircrafts.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.AircraftName = model.AircraftName;
            element.Price = model.Price;
            int maxAPId = source.AircraftParts.Count > 0 ? source.AircraftParts.Max(rec => rec.Id) : 0;
            var compIds = model.AircraftParts.Select(rec => rec.PartId).Distinct();
            var updateParts = source.AircraftParts.Where(rec => rec.AircraftId == model.Id && compIds.Contains(rec.PartId));
            foreach (var updatePart in updateParts)
            {
                updatePart.Count = model.AircraftParts.FirstOrDefault(rec => rec.Id == updatePart.Id).Count;
            }
            source.AircraftParts.RemoveAll(rec => rec.AircraftId == model.Id && !compIds.Contains(rec.PartId));
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
                AircraftPart elementPC = source.AircraftParts.FirstOrDefault(rec => rec.AircraftId == model.Id && rec.PartId == groupPart.PartId);
                if (elementPC != null)
                {
                    elementPC.Count += groupPart.Count;
                }
                else
                {
                    source.AircraftParts.Add(new AircraftPart
                    {
                        Id = ++maxAPId,
                        AircraftId = model.Id,
                        PartId = groupPart.PartId,
                        Count = groupPart.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Aircraft element = source.Aircrafts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.AircraftParts.RemoveAll(rec => rec.AircraftId == id);
                source.Aircrafts.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }

}
