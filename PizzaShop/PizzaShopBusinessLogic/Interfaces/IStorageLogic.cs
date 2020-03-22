using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.BindingModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> Read(StorageBindingModel model);
        void CreateOrUpdate(StorageBindingModel model);
        void Delete(StorageBindingModel model);
    }
}
