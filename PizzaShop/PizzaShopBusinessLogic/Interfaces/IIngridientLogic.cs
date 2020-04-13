using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IIngridientLogic
    {
        List<IngridientViewModel> Read(IngridientBindingModel model);
        void CreateOrUpdate(IngridientBindingModel model);
        void Delete(IngridientBindingModel model);
    }
}
