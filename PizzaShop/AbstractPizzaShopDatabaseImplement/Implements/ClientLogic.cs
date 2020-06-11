using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopDatabaseImplement.Models;

namespace PizzaShopDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec =>
               rec.Login == model.Login && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть клиент с таким логином");
                }
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.Login = model.Login;
                element.FIO = model.FIO;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                return context.Clients
                .Where(rec => model == null
                || rec.Id == model.Id 
                || rec.Login == model.Login && rec.Password == model.Password)
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    FIO = rec.FIO,
                    Login = rec.Login,
                    Password = rec.Password
                })
                .ToList();
            }
        }

    }
}
