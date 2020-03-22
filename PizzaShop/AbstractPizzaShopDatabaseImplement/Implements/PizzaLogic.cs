using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopDatabaseImplement.Models;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.BindingModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PizzaShopDatabaseImplement.Implements
{
    public class PizzaLogic : IPizzaShopLogic
    {
        public void CreateOrUpdate(ProductBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Pizza element = context.Pizzas.FirstOrDefault(rec => rec.PizzaName == model.PizzaName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Pizza();
                            context.Pizzas.Add(element);
                        }
                        element.PizzaName = model.PizzaName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productComponents = context.PizzaIngridients.Where(rec => rec.PizzaId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.PizzaIngridients.RemoveRange(productComponents.Where(rec => !model.PizzaIngridients.ContainsKey(rec.IngridientId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateComponent in productComponents)
                            {
                                updateComponent.Count = model.PizzaIngridients[updateComponent.IngridientId].Item2;
                                model.PizzaIngridients.Remove(updateComponent.IngridientId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.PizzaIngridients)
                        {
                            context.PizzaIngridients.Add(new PizzaIngridient
                            {
                                PizzaId = element.Id,
                                IngridientId = pc.Key,
                                Count = pc.Value.Item2
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
        public void Delete(ProductBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.PizzaIngridients.RemoveRange(context.PizzaIngridients.Where(rec => rec.PizzaId == model.Id));
                        Pizza element = context.Pizzas.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Pizzas.Remove(element);
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
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                return context.Pizzas
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new ProductViewModel
                {
                    Id = rec.Id,
                    PizzaName = rec.PizzaName,
                    Price = rec.Price,
                    PizzaIngridients = context.PizzaIngridients
                .Include(recPC => recPC.Ingridient)
                .Where(recPC => recPC.PizzaId == rec.Id)
                .ToDictionary(recPC => recPC.IngridientId, recPC =>
                (recPC.Ingridient?.IngridientName, recPC.Count))
                })
                .ToList();
            }
        }
    }
}
