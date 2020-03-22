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

        public void CreateOrUpdate(StorageBindingModel storage)
        {
            Storage tempStorage = storage.Id.HasValue ? null : new Storage
            {
                Id = 1
            };
            foreach (var s in source.Storages)
            {
                if (s.StorageName == storage.StorageName && s.Id != storage.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                if (!storage.Id.HasValue && s.Id >= tempStorage.Id)
                {
                    tempStorage.Id = s.Id + 1;
                }
                else if (storage.Id.HasValue && s.Id == storage.Id)
                {
                    tempStorage = s;
                }
            }
            if (storage.Id.HasValue)
            {
                if (tempStorage == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(storage, tempStorage);
            }
            else
            {
                source.Storages.Add(CreateModel(storage, tempStorage));
            }
        }

        public void Delete(StorageBindingModel model)
        {
            // удаляем записи по компонентам при удалении хранилища
            for (int i = 0; i < source.StorageIngridients.Count; ++i)
            {
                if (source.StorageIngridients[i].StorageId == model.Id)
                {
                    source.StorageIngridients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            foreach (var storage in source.Storages)
            {
                if (model != null)
                {
                    if (storage.Id == model.Id)
                    {
                        result.Add(CreateViewModel(storage));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(storage));
            }
            return result;
        }

        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxSMId = 0;
            for (int i = 0; i < source.StorageIngridients.Count; ++i)
            {
                if (source.StorageIngridients[i].Id > maxSMId)
                {
                    maxSMId = source.StorageIngridients[i].Id;
                }
                if (source.StorageIngridients[i].StorageId == storage.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if (model.StoragedMaterials.ContainsKey(source.StorageIngridients[i].MaterialId))
                    {
                        // обновляем количество
                        source.StorageIngridients[i].Count = model.StoragedMaterials[source.StorageMaterials[i].MaterialId].Item2;
                        // из модели убираем эту запись, чтобы остались только не
                        //просмотренные
                        model.StoragedMaterials.Remove(source.StorageIngridients[i].MaterialId);
                    }
                    else
                    {
                        source.StorageIngridients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var sm in model.StoragedMaterials)
            {
                source.StorageIngridients.Add(new StorageIngridient
                {
                    Id = ++maxSMId,
                    StorageId = storage.Id,
                    MaterialId = sm.Key,
                    Count = sm.Value.Item2
                });
            }
            return storage;
        }

        private StorageViewModel CreateViewModel(Storage storage)
        {
            // требуется дополнительно получить список компонентов для хранилища с
            // названиями и их количество
            Dictionary<int, (string, int)> storageMaterials = new Dictionary<int, (string, int)>();
            foreach (var sm in source.StorageIngridients)
            {
                if (sm.StorageId == storage.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Ingridients)
                    {
                        if (sm.MaterialId == component.Id)
                        {
                            componentName = component.MaterialName;
                            break;
                        }
                    }
                    storageMaterials.Add(sm.MaterialId, (componentName, sm.Count));
                }
            }
            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                StoragedMaterials = storageMaterials
            };
        }
    }
}
