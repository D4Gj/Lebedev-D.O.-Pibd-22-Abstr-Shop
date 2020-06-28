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
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == model.Id)
                {
                    source.PizzaIngredients.RemoveAt(i--);
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
        private Pizza CreateModel(PizzaBindingModel model, Pizza product)
        {
            product.PizzaName = model.PizzaName;
            product.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredients[i].Id;
                }
                if (source.PizzaIngredients[i].PizzaId == product.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.PizzaIngridients.ContainsKey(source.PizzaIngredients[i].IngridientID))
                    {
                        // обновляем количество
                        source.PizzaIngredients[i].Count =
                        model.PizzaIngridients[source.PizzaIngredients[i].IngridientID].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные

                        model.PizzaIngridients.Remove(source.PizzaIngredients[i].IngridientID);
                    }
                    else
                    {
                        source.PizzaIngredients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.PizzaIngridients)
            {
                source.PizzaIngredients.Add(new PizzaIngridient
                {
                    Id = ++maxPCId,
                    PizzaId = product.Id,
                    IngridientID = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return product;
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var component in source.Pizzas)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private PizzaViewModel CreateViewModel(Pizza product)
        {
            // требуется дополнительно получить список компонентов для изделия с  названиями и их количество
            Dictionary<int, (string, int)> pizzaIngridients = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.PizzaIngredients)
            {
                if (pc.PizzaId == product.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Ingridients)
                    {
                        if (pc.IngridientID == component.Id)
                        {
                            componentName = component.IngridientName;
                            break;
                        }
                    }
                    pizzaIngridients.Add(pc.IngridientID, (componentName, pc.Count));
                }
            }
            return new PizzaViewModel
            {
                Id = product.Id,
                PizzaName = product.PizzaName,
                Price = product.Price,
                PizzaIngridients = pizzaIngridients
            };
        }
    }
}
