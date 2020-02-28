using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IPizzaShopLogic
    {
        List<ProductViewModel> Read(ProductBindingModel model);
        void CreateOrUpdate(ProductBindingModel model);
        void Delete(ProductBindingModel model);
    }
}
