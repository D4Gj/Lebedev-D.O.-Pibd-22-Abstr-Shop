using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopListImplement.Models;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.Interfaces;

namespace PizzaShopListImplement.Implements
{
    public class ProductLogic : IPizzaShopLogic
    {
        private readonly DataListSingleton source;
        public ProductLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza tempProduct = model.Id.HasValue ? null : new Pizza { Id = 1 };
            foreach (var product in source.Pizzas)
            {
                if (product.PizzaName == model.PizzaName && product.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
                else if (model.Id.HasValue && product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Pizzas.Add(CreateModel(model, tempProduct));
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
                source.PizzaIngredients.Add(new PizzaIngredient
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
