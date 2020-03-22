using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopDatabaseImplement.Models;
using System.Linq;

namespace PizzaShopDatabaseImplement.Implements
{
    class IngridientLogic : IIngridientLogic
    {
        public void CreateOrUpdate(IngridientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                Ingridient element = context.Ingridients.FirstOrDefault(rec =>
               rec.IngridientName == model.IngridientName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Ingridients.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                }
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }

                else
                {
                    element = new Ingridient();
                    context.Ingridients.Add(element);
                }
                element.IngridientName = model.IngridientName;
                context.SaveChanges();
            }
        }
        public void Delete(IngridientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                Ingridient element = context.Ingridients.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Ingridients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<IngridientViewModel> Read(IngridientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                return context.Ingridients
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new IngridientViewModel
                {
                    Id = rec.Id,
                    IngridientName = rec.IngridientName
                })
                .ToList();
            }
        }
    }
}

