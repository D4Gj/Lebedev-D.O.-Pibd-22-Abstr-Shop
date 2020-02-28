using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> GetList();
        ComponentViewModel GetElement(int id);
        void AddElement(ComponentBindingModel model);
        void UpdElement(ComponentBindingModel model);
        void DelElement(int id);
    }
}
