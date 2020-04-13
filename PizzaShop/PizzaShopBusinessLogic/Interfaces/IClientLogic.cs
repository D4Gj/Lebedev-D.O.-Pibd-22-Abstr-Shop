using System.Collections.Generic;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientBindingModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
