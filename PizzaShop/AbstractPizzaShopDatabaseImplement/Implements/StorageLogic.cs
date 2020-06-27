using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopDatabaseImplement.Models;

namespace PizzaShopDatabaseImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        public List<StorageViewModel> GetList()
        {
            using (var context = new PizzaShopDatabase())
            {
                return context.Storages
                .ToList()
               .Select(rec => new StorageViewModel
               {
                   Id = rec.Id,
                   StorageName = rec.StorageName,
                   StorageIngridients = context.StorageIngridients
                .Include(recSF => recSF.Ingridient)
               .Where(recSF => recSF.StorageId == rec.Id).
               Select(x => new StorageIngridientViewModel
               {
                   Id = x.Id,
                   StorageId = x.StorageId,
                   IngridientId = x.IngridientId,
                   IngridientName = context.Ingridients.FirstOrDefault(y => y.Id == x.IngridientId).IngridientName,
                   Count = x.Count
               })
               .ToList()
               })
            .ToList();
            }
        }

        public StorageViewModel GetElement(int id)
        {
            using (var context = new PizzaShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
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
                        StorageIngridients = context.StorageIngridients
                .Include(recSF => recSF.Ingridient)
               .Where(recSF => recSF.StorageId == elem.Id)
                        .Select(x => new StorageIngridientViewModel
                        {
                            Id = x.Id,
                            StorageId = x.StorageId,
                            IngridientId = x.IngridientId,
                            IngridientName = context.Ingridients.FirstOrDefault(y => y.Id == x.IngridientId).IngridientName,
                            Count = x.Count
                        }).ToList()
                    };
                }
            }
        }

        public void AddElement(StorageBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var storage = new Storage();
                context.Storages.Add(storage);
                storage.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }

        public void UpdElement(StorageBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var elemToUpdate = context.Storages.FirstOrDefault(x => x.Id == model.Id);
                if (elemToUpdate != null)
                {
                    elemToUpdate.StorageName = model.StorageName;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void DelElement(int id)
        {
            using (var context = new PizzaShopDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem != null)
                {
                    context.Storages.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void ReplenishStorage(StorageIngridientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                var item = context.StorageIngridients.FirstOrDefault(x => x.IngridientId == model.IngridientId
    && x.StorageId == model.StorageId);

                if (item != null)
                {
                    item.Count += model.Count;
                }
                else
                {
                    var elem = new StorageIngridient();
                    context.StorageIngridients.Add(elem);
                    elem.StorageId = model.StorageId;
                    elem.IngridientId = model.IngridientId;
                    elem.Count = model.Count;
                }
                context.SaveChanges();
            }
        }

        public void RemoveFromStorage(int pizzaId, int pizzasCount)
        {
            using (var context = new PizzaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var pizzaIngridients = context.PizzaIngridients.Where(x => x.PizzaId == pizzaId);
                        if (pizzaIngridients.Count() == 0) return;
                        foreach (var elem in pizzaIngridients)
                        {
                            int left = elem.Count * pizzasCount;
                            var storageIngridients = context.StorageIngridients.Where(x => x.IngridientId == elem.IngridientId);
                            int available = storageIngridients.Sum(x => x.Count);
                            if (available < left) throw new Exception("Недостаточно заготовок на складе");
                            foreach (var rec in storageIngridients)
                            {
                                int toRemove = left > rec.Count ? rec.Count : left;
                                rec.Count -= toRemove;
                                left -= toRemove;
                                if (left == 0) break;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
