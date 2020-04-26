using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopListImplement.Models;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.Interfaces;


namespace PizzaShopListImplement.Implements
{
    public class IngridientLogic : IIngridientLogic
    {
        private readonly DataListSingleton source;
        public IngridientLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        
        public void CreateOrUpdate(IngridientBindingModel model)
        {
            Ingridient tempComponent = model.Id.HasValue ? null : new Ingridient
            {
                Id = 1
            };
            foreach (var component in source.Ingridients)
            {
                if (component.IngridientName == model.IngridientName && component.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть ингредиент с таким названием");
                }
                if (!model.Id.HasValue && component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
                else if (model.Id.HasValue && component.Id == model.Id)
                {
                    tempComponent = component;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempComponent == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempComponent);
            }
            else
            {
                source.Ingridients.Add(CreateModel(model, tempComponent));
            }
        }
        public void Delete(IngridientBindingModel model)
        {
            for (int i = 0; i < source.Ingridients.Count; ++i)
            {
                if (source.Ingridients[i].Id == model.Id.Value)
                {
                    source.Ingridients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<IngridientViewModel> Read(IngridientBindingModel model)
        {
            List<IngridientViewModel> result = new List<IngridientViewModel>();
            foreach (var component in source.Ingridients)
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
        private Ingridient CreateModel(IngridientBindingModel model, Ingridient component)
        {
            component.IngridientName = model.IngridientName;
            return component;
        }
        private IngridientViewModel CreateViewModel(Ingridient component)
        {
            return new IngridientViewModel
            {
                Id = component.Id,
                IngridientName = component.IngridientName
            };
        }
    }
}


