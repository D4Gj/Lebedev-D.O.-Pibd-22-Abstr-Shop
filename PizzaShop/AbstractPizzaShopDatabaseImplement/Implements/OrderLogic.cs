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
                Order order = context.Orders.FirstOrDefault(rec => rec.Id != model.Id);
                if (model.Id.HasValue)
                {
                    order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (order == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    order.PizzaId = model.PizzaId;
                    order.Status = model.Status;
                    order.PizzaId = model.PizzaId;
                    order.Count = model.Count;
                    order.Sum = model.Sum;
                    order.DateCreate = model.DateCreate;
                    order.DateImplement = model.DateImplement;
                }
                else
                {
                    order = new Order();
                    order.PizzaId = model.PizzaId;
                    order.Status = model.Status;
                    order.PizzaId = model.PizzaId;
                    order.Count = model.Count;
                    order.Sum = model.Sum;
                    order.DateCreate = model.DateCreate;
                    order.DateImplement = model.DateImplement;
                    context.Orders.Add(order);
                }
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
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    Count = rec.Count,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    PizzaName = context.Pizzas.FirstOrDefault((r) => r.Id == rec.PizzaId).PizzaName,
                    PizzaId = rec.PizzaId,
                    Status = rec.Status,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}
