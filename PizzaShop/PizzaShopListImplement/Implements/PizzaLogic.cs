using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopListImplement.Models;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.Interfaces;
using System.Linq;

namespace PizzaShopListImplement.Implements
{
    public class PizzaLogic : IPizzaShopLogic
    {
        private readonly DataListSingleton source;
        public PizzaLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var ingridient in source.Pizzas)
            {
                if (model != null)
                {
                    if (ingridient.Id == model.Id)
                    {
                        result.Add(CreateViewModel(ingridient));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(ingridient));
            }
            return result;
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.PizzaName ==
            model.PizzaName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть пицца с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Pizzas.Count > 0 ? source.Ingridients.Max(rec =>
               rec.Id) : 0;
                element = new Pizza { Id = maxId + 1 };
                source.Pizzas.Add(element);
            }
            element.PizzaName = model.PizzaName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.PizzaIngredients.RemoveAll(rec => rec.PizzaId == model.Id &&
           !model.PizzaIngridients.ContainsKey(rec.IngridientID));
            // обновили количество у существующих записей
            var updateComponents = source.PizzaIngredients.Where(rec => rec.IngridientID ==
           model.Id && model.PizzaIngridients.ContainsKey(rec.IngridientID));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.PizzaIngridients[updateComponent.IngridientID].Item2;
                model.PizzaIngridients.Remove(updateComponent.IngridientID);
            }
            // добавили новые
            int maxPCId = source.PizzaIngredients.Count > 0 ?
           source.PizzaIngredients.Max(rec => rec.Id) : 0;
            foreach (var pc in model.PizzaIngridients)
            {
                source.PizzaIngredients.Add(new PizzaIngridient
                {
                    Id = ++maxPCId,
                    PizzaId = element.Id,
                    IngridientID = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(PizzaBindingModel model)
        {            
            for (int i = 0; i < source.PizzaIngridients.Count; ++i)
            {
                if (source.PizzaIngridients[i].PizzaId == model.Id)
                {
                    source.PizzaIngridients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }       
        private Pizza CreateModel(PizzaBindingModel model, Pizza Pizza)
        {
            Pizza.PizzaName = model.PizzaName;
            Pizza.Price = model.Price;           
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngridients.Count; ++i)
            {
                if (source.PizzaIngridients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngridients[i].Id;
                }
                if (source.PizzaIngridients[i].PizzaId == Pizza.Id)
                {
                    
                    if
                    (model.PizzaIngridients.ContainsKey(source.PizzaIngridients[i].IngridientID))
                    {
                       
                        source.PizzaIngridients[i].Count =
                        model.PizzaIngridients[source.PizzaIngridients[i].IngridientID].Item2;
                        model.PizzaIngridients.Remove(source.PizzaIngridients[i].IngridientID);
                    }
                    else
                    {
                        source.PizzaIngridients.RemoveAt(i--);
                    }
                }
            }
           
            foreach (var pc in model.PizzaIngridients)
            {
                source.PizzaIngredients.Add(new PizzaIngridient
                {
                    Id = ++maxPCId,
                    PizzaId = Pizza.Id,
                    IngridientID = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return Pizza;
        }
        private PizzaViewModel CreateViewModel(Pizza Pizza)
        {            
            Dictionary<int, (string, int)> pizzaIngridients = new Dictionary<int,(string, int)>();
            foreach (var pc in source.PizzaIngridients)
            {
                if (pc.PizzaId == Pizza.Id)
                {
                    string IngridientName = string.Empty;
                    foreach (var Ingridient in source.Ingridients)
                    {
                        if (pc.IngridientID == Ingridient.Id)
                        {
                            IngridientName = Ingridient.IngridientName;
                            break;
                        }
                    }
                    pizzaIngridients.Add(pc.IngridientID, (IngridientName, pc.Count));
                }
            }
            return new PizzaViewModel
            {
                Id = Pizza.Id,
                PizzaName = Pizza.PizzaName,
                Price = Pizza.Price,
                PizzaIngridients = pizzaIngridients
            };
        }
    }
}
