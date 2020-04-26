using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopListImplement.Models;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.Interfaces;

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
                source.Pizzas.Add(CreateModel(model,tempProduct));
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
                source.PizzaIngridients.Add(new PizzaIngridient
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
