using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IPizzaShopLogic
    {
        List<PizzaViewModel> Read(PizzaBindingModel model);
        void CreateOrUpdate(PizzaBindingModel model);
        void Delete(PizzaBindingModel model);
    }
}
