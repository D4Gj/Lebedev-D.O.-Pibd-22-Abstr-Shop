using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShopRestApi.Models;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using PizzaShopBusinessLogic.BusinessLogic;

namespace PizzaShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IPizzaShopLogic _pizza;
        private readonly MainLogic _main;

        public MainController(IOrderLogic order, IPizzaShopLogic product, MainLogic main)
        {
            _order = order;
            _pizza = product;
            _main = main;
        }
        [HttpGet]
        public List<PizzaViewModel> GetProductList() => _pizza.Read(null);
        [HttpGet]
        public PizzaViewModel GetProduct(int pizzaId) => _pizza.Read(new PizzaBindingModel
        {
            Id = pizzaId
        })?.FirstOrDefault();
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
    }
}