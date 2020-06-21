using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftFactoryListImplement
{
    public class AircraftLogic : IAircraftLogic
    {
        private readonly DataListSingleton source;

        public AircraftLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<AircraftViewModel> GetList()
        {
            List<AircraftViewModel> result = new List<AircraftViewModel>();
            for (int i = 0; i < source.Aircrafts.Count; ++i)
            {
                List<AircraftPartViewModel> aircraftParts = new List<AircraftPartViewModel>();

                for (int j = 0; j < source.AircraftParts.Count; ++j)
                {
                    if (source.AircraftParts[j].AircraftId == source.Aircrafts[i].Id)
                    {
                        string partName = string.Empty;

                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.AircraftParts[j].PartId == source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;

                                break;
                            }
                        }

                        aircraftParts.Add(new AircraftPartViewModel
                        {
                            Id = source.AircraftParts[j].Id,
                            AircraftId = source.AircraftParts[j].AircraftId,
                            PartId = source.AircraftParts[j].PartId,
                            PartName = partName,
                            Count = source.AircraftParts[j].Count
                        });
                    }
                }
                result.Add(new AircraftViewModel
                {
                    Id = source.Aircrafts[i].Id,
                    AircraftName = source.Aircrafts[i].AircraftName,
                    Price = source.Aircrafts[i].Price,
                    AircraftParts = aircraftParts
                });
            }

            return result;
        }

        public AircraftViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Aircrafts.Count; ++i)
            {
                List<AircraftPartViewModel> aircraftParts = new List<AircraftPartViewModel>();

                for (int j = 0; j < source.AircraftParts.Count; ++j)
                {
                    if (source.AircraftParts[j].AircraftId == source.Aircrafts[i].Id)
                    {
                        string partName = string.Empty;

                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.AircraftParts[j].PartId == source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;

                                break;
                            }
                        }

                        aircraftParts.Add(new AircraftPartViewModel
                        {
                            Id = source.AircraftParts[j].Id,
                            AircraftId = source.AircraftParts[j].AircraftId,
                            PartId = source.AircraftParts[j].PartId,
                            PartName = partName,
                            Count = source.AircraftParts[j].Count
                        });
                    }
                }

                if (source.Aircrafts[i].Id == id)
                {
                    return new AircraftViewModel
                    {
                        Id = source.Aircrafts[i].Id,
                        AircraftName = source.Aircrafts[i].AircraftName,
                        Price = source.Aircrafts[i].Price,
                        AircraftParts = aircraftParts
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(AircraftBindingModel model)
        {
            int maxId = 0;

            for (int i = 0; i < source.Aircrafts.Count; ++i)
            {
                if (source.Aircrafts[i].Id > maxId)
                {
                    maxId = source.Aircrafts[i].Id;
                }

                if (source.Aircrafts[i].AircraftName == model.AircraftName)
                {
                    throw new Exception("Уже есть самолёт с таким названием");
                }
            }

            source.Aircrafts.Add(new Aircraft
            {
                Id = maxId + 1,
                AircraftName = model.AircraftName,
                Price = model.Price
            });

            int maxAId = 0;

            for (int i = 0; i < source.AircraftParts.Count; ++i)
            {
                if (source.AircraftParts[i].Id > maxAId)
                {
                    maxAId = source.AircraftParts[i].Id;
                }
            }

            for (int i = 0; i < model.AircraftParts.Count; ++i)
            {
                for (int j = 1; j < model.AircraftParts.Count; ++j)
                {
                    if (model.AircraftParts[i].PartId == model.AircraftParts[j].PartId)
                    {
                        model.AircraftParts[i].Count +=
                        model.AircraftParts[j].Count;
                        model.AircraftParts.RemoveAt(j--);
                    }
                }
            }

            for (int i = 0; i < model.AircraftParts.Count; ++i)
            {
                source.AircraftParts.Add(new AircraftPart
                {
                    Id = ++maxAId,
                    AircraftId = maxId + 1,
                    PartId = model.AircraftParts[i].PartId,
                    Count = model.AircraftParts[i].Count
                });
            }
        }
        public void UpdElement(AircraftBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Aircrafts.Count; ++i)
            {
                if (source.Aircrafts[i].Id == model.Id)
                {
                    index = i;
                }

                if (source.Aircrafts[i].AircraftName == model.AircraftName &&
                source.Aircrafts[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            source.Aircrafts[index].AircraftName = model.AircraftName;
            source.Aircrafts[index].Price = model.Price;
            int maxAId = 0;

            for (int i = 0; i < source.AircraftParts.Count; ++i)
            {
                if (source.AircraftParts[i].Id > maxAId)
                {
                    maxAId = source.AircraftParts[i].Id;
                }
            }

            for (int i = 0; i < source.AircraftParts.Count; ++i)
            {
                if (source.AircraftParts[i].AircraftId == model.Id)
                {
                    bool flag = true;

                    for (int j = 0; j < model.AircraftParts.Count; ++j)
                    {
                        if (source.AircraftParts[i].Id ==
                        model.AircraftParts[j].Id)
                        {
                            source.AircraftParts[i].Count =
                            model.AircraftParts[j].Count;
                            flag = false;

                            break;
                        }
                    }

                    if (flag)
                    {
                        source.AircraftParts.RemoveAt(i--);
                    }
                }
            }

            for (int i = 0; i < model.AircraftParts.Count; ++i)
            {
                if (model.AircraftParts[i].Id == 0)
                {
                    for (int j = 0; j < source.AircraftParts.Count; ++j)
                    {
                        if (source.AircraftParts[j].AircraftId == model.Id &&
                        source.AircraftParts[j].PartId == model.AircraftParts[i].PartId)
                        {
                            source.AircraftParts[j].Count +=
                            model.AircraftParts[i].Count;
                            model.AircraftParts[i].Id = source.AircraftParts[j].Id;

                            break;
                        }
                    }

                    if (model.AircraftParts[i].Id == 0)
                    {
                        source.AircraftParts.Add(new AircraftPart
                        {
                            Id = ++maxAId,
                            AircraftId = model.Id,
                            PartId = model.AircraftParts[i].PartId,
                            Count = model.AircraftParts[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.AircraftParts.Count; ++i)
            {
                if (source.AircraftParts[i].AircraftId == id)
                {
                    source.AircraftParts.RemoveAt(i--);
                }
            }

            for (int i = 0; i < source.Aircrafts.Count; ++i)
            {
                if (source.Aircrafts[i].Id == id)
                {
                    source.Aircrafts.RemoveAt(i);

                    return;
                }
            }

            throw new Exception("Элемент не найден");
        }
    }

}
