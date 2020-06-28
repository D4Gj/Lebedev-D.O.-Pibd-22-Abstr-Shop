using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopFileImplement.Models;
using System.Linq;

namespace PizzaShopFileImplement.Implements
{
    public class IngridientLogic : IIngridientLogic
    {
        private readonly FileDataListSingleton source;
        public IngridientLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(IngridientBindingModel model)
        {
            Ingridient element = source.Ingridients.FirstOrDefault(rec => rec.IngridientName
           == model.IngridientName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть ингредиент с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Ingridients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Ingridients.Count > 0 ? source.Ingridients.Max(rec =>
               rec.Id) : 0;
                element = new Ingridient { Id = maxId + 1 };
                source.Ingridients.Add(element);
            }
            element.IngridientName = model.IngridientName;
        }
        public void Delete(IngridientBindingModel model)
        {
            Ingridient element = source.Ingridients.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Ingridients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<IngridientViewModel> Read(IngridientBindingModel model)
        {
            return source.Ingridients
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
