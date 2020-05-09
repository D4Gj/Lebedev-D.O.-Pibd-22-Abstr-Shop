using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.BindingModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        List<StorageViewModel> GetList();

        StorageViewModel GetElement(int id);

        void AddElement(StorageBindingModel model);

        void UpdElement(StorageBindingModel model);

        void DelElement(int id);

        void AddComponent(StorageIngridientBindingModel model);

        bool IsIngridientAvailible(int pizzaId, int pizzasCount);
        void RemoveFromStorage(int pizzaId, int pizzasCount);

    }
}
