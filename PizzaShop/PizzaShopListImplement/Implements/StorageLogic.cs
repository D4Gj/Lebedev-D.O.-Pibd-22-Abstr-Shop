using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopListImplement.Models;

namespace PizzaShopListImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly DataListSingleton source;

        public StorageLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();

            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageIngridientViewModel> storageIngridients = new List<StorageIngridientViewModel>();

                for (int j = 0; j < source.StorageIngridients.Count; ++j)
                {
                    if (source.StorageIngridients[j].StorageId == source.Storages[i].Id)
                    {
                        string ingridientName = string.Empty;

                        for (int k = 0; k < source.Ingridients.Count; ++k)
                        {
                            if (source.StorageIngridients[j].IngridientId == source.Ingridients[k].Id)
                            {
                                ingridientName = source.Ingridients[k].IngridientName;
                                break;
                            }
                        }

                        storageIngridients.Add(new StorageIngridientViewModel
                        {
                            Id = source.StorageIngridients[j].Id,
                            StorageId = source.StorageIngridients[j].StorageId,
                            IngridientId = source.StorageIngridients[j].IngridientId,
                            IngridientName = ingridientName,
                            Count = source.StorageIngridients[j].Count
                        });
                    }
                }

                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageIngridients = storageIngridients
                });
            }

            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageIngridientViewModel> storageIngridients = new List<StorageIngridientViewModel>();

                for (int j = 0; j < source.StorageIngridients.Count; ++j)
                {
                    if (source.StorageIngridients[j].StorageId == source.Storages[i].Id)
                    {
                        string ingridientName = string.Empty;

                        for (int k = 0; k < source.Ingridients.Count; ++k)
                        {
                            if (source.StorageIngridients[j].IngridientId == source.Ingridients[k].Id)
                            {
                                ingridientName = source.Ingridients[k].IngridientName;
                                break;
                            }
                        }

                        storageIngridients.Add(new StorageIngridientViewModel
                        {
                            Id = source.StorageIngridients[j].Id,
                            StorageId = source.StorageIngridients[j].StorageId,
                            IngridientId = source.StorageIngridients[j].IngridientId,
                            IngridientName = ingridientName,
                            Count = source.StorageIngridients[j].Count
                        });
                    }
                }

                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageIngridients = storageIngridients
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindingModel model)
        {
            int maxId = 0;

            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }

                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }

            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(StorageBindingModel model)
        {
            int index = -1;

            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }

                if (source.Storages[i].StorageName == model.StorageName && source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            source.Storages[index].StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.StorageIngridients.Count; ++i)
            {
                if (source.StorageIngridients[i].StorageId == id)
                {
                    source.StorageIngridients.RemoveAt(i--);
                }
            }

            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddComponent(StorageIngridientBindingModel model)
        {
            int findItemIndex = -1;
            for (int i = 0; i < source.StorageIngridients.Count; ++i)
            {
                if (source.StorageIngridients[i].IngridientId == model.IngridientId
                    && source.StorageIngridients[i].StorageId == model.StorageId)
                {
                    findItemIndex = i;
                    break;
                }
            }
            if (findItemIndex != -1)
            {
                source.StorageIngridients[findItemIndex].Count =
                    source.StorageIngridients[findItemIndex].Count + model.Count;
            }
            else
            {
                int maxId = 0;
                for (int i = 0; i < source.StorageIngridients.Count; ++i)
                {
                    if (source.StorageIngridients[i].Id > maxId)
                    {
                        maxId = source.StorageIngridients[i].Id;
                    }
                }
                source.StorageIngridients.Add(new StorageIngridient
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    IngridientId = model.IngridientId,
                    Count = model.Count
                });
            }
        }
    }
}

