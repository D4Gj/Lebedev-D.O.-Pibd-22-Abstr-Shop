using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopFileImplement.Models;
using System.Linq;

namespace PizzaShopFileImplement.Implements
{
    public class StorageLogic: IStorageLogic
    {
        private readonly FileDataListSingleton source;
        public StorageLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            return source.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageIngridients = source.StorageIngridients.Where(z => z.StorageId == rec.Id).Select(x => new StorageIngridientViewModel
                {
                    Id = x.Id,
                    StorageId = x.StorageId,
                    IngridientId = x.IngridientId,
                    IngridientName = source.Ingridients.FirstOrDefault(y => y.Id == x.IngridientId)?.IngridientName,
                    Count = x.Count
                }).ToList()
            })
               .ToList();
        }
        public StorageViewModel GetElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
            if (elem == null)
            {
                throw new Exception("Элемент не найден");
            }
            else
            {
                return new StorageViewModel
                {
                    Id = id,
                    StorageName = elem.StorageName,
                    StorageIngridients = source.StorageIngridients.Where(z => z.StorageId == elem.Id).Select(x => new StorageIngridientViewModel
                    {
                        Id = x.Id,
                        StorageId = x.StorageId,
                        IngridientId = x.IngridientId,
                        IngridientName = source.Ingridients.FirstOrDefault(y => y.Id == x.IngridientId)?.IngridientName,
                        Count = x.Count
                    }).ToList()
                };
            }
        }
        public void AddElement(StorageBindingModel model)
        {

            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            var elemToUpdate = source.Storages.FirstOrDefault(x => x.Id == model.Id);
            if (elemToUpdate != null)
            {
                elemToUpdate.StorageName = model.StorageName;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void DelElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
            if (elem != null)
            {
                source.Storages.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void ReplenishStorage(StorageIngridientBindingModel model)
        {
            var item = source.StorageIngridients.FirstOrDefault(x => x.IngridientId == model.IngridientId
                   && x.StorageId == model.StorageId);

            if (item != null)
            {
                item.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageIngridients.Count > 0 ? source.StorageIngridients.Max(rec => rec.Id) : 0;
                source.StorageIngridients.Add(new StorageIngridient
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    IngridientId = model.IngridientId,
                    Count = model.Count
                });
            }
        }
        
        public bool IsIngridientAvailible(int pizzaId, int pizzasCount)
        {
            bool result = true;
            var PizzaIngridients = source.PizzaIngridients.Where(x => x.PizzaId == pizzaId);
            if (PizzaIngridients.Count() == 0) return false;
            foreach (var elem in PizzaIngridients)
            {
                int count = 0;
                var storageIngridients = source.StorageIngridients.FindAll(x => x.IngridientId == elem.IngridientId);
                count = storageIngridients.Sum(x => x.Count);
                if (count < elem.Count * pizzasCount)
                    return false;
            }
            return result;
        }
        public void RemoveFromStorage(int pizzaId, int pizzasCount)
        {
            var PizzaIngridients = source.PizzaIngridients.Where(x => x.PizzaId == pizzaId);
            if (PizzaIngridients.Count() == 0) return;
            foreach (var elem in PizzaIngridients)
            {
                int left = elem.Count * pizzasCount;
                var storageIngridients = source.StorageIngridients.FindAll(x => x.IngridientId == elem.IngridientId);
                foreach (var rec in storageIngridients)
                {
                    int toRemove = left > rec.Count ? rec.Count : left;
                    rec.Count -= toRemove;
                    left -= toRemove;
                    if (left == 0) break;
                }
            }
            return;
        }
    }
}
