using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopDatabaseImplement.Models;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PizzaShopDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                Order element;

                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }

                element.PizzaId = model.PizzaId == 0 ? element.PizzaId : model.PizzaId;
                element.ClientId = model.ClientId == null ? element.ClientId : (int)model.ClientId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;

                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new PizzaShopDatabase())
            {
                return context.Orders
                .Where(
                    rec => model == null
                    || rec.Id == model.Id && model.Id.HasValue
                    || model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo
                    || model.ClientId.HasValue && rec.ClientId == model.ClientId
                )
                .Include(rec => rec.Pizza)
                .Include(rec => rec.Client)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    PizzaId = rec.PizzaId,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    PizzaName = rec.Pizza.PizzaName,
                    ClientFIO = rec.Client.FIO
                })
                .ToList();
            }
        }
    }
}
