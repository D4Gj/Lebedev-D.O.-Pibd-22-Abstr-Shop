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
    public class PizzaLogic : IPizzaShopLogic
    {
        private readonly FileDataListSingleton source;
        public PizzaLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza element = source.Pizza.FirstOrDefault(rec => rec.PizzaName ==
           model.PizzaName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть пицца с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Pizza.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Pizza.Count > 0 ? source.Ingridients.Max(rec =>
               rec.Id) : 0;
                element = new Pizza { Id = maxId + 1 };
                source.Pizza.Add(element);
            }
            element.PizzaName = model.PizzaName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.PizzaIngridients.RemoveAll(rec => rec.PizzaId == model.Id &&
           !model.PizzaIngridients.ContainsKey(rec.IngridientId));
            // обновили количество у существующих записей
            var updateComponents = source.PizzaIngridients.Where(rec => rec.IngridientId ==
           model.Id && model.PizzaIngridients.ContainsKey(rec.IngridientId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.PizzaIngridients[updateComponent.IngridientId].Item2;
                model.PizzaIngridients.Remove(updateComponent.IngridientId);
            }
            // добавили новые
            int maxPCId = source.PizzaIngridients.Count > 0 ?
           source.PizzaIngridients.Max(rec => rec.Id) : 0;
            foreach (var pc in model.PizzaIngridients)
            {
                source.PizzaIngridients.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = element.Id,
                    IngridientId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(PizzaBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.PizzaIngridients.RemoveAll(rec => rec.PizzaId == model.Id);
            Pizza element = source.Pizza.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Pizza.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            return source.Pizza
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new PizzaViewModel
            {
                Id = rec.Id,
                PizzaName = rec.PizzaName,
                Price = rec.Price,
                PizzaIngridients = source.PizzaIngridients
            .Where(recPC => recPC.PizzaId == rec.Id)
           .ToDictionary(recPC => recPC.IngridientId, recPC =>
            (source.Ingridients.FirstOrDefault(recC => recC.Id ==
           recPC.IngridientId)?.IngridientName, recPC.Count))
            })
            .ToList();
        }
    }
}
