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
        private readonly IPizzaShopLogic _product;
        private readonly MainLogic _main;

        public MainController(IOrderLogic order, IPizzaShopLogic product, MainLogic main)
        {
            _order = order;
            _product = product;
            _main = main;
        }
        [HttpGet]
        public List<PizzaModel> GetProductList() => _product.Read(null)?.Select(rec => Convert(rec)).ToList();
        [HttpGet]
        public PizzaModel GetProduct(int productId) => Convert(_product.Read(new PizzaBindingModel { Id = productId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
        private PizzaModel Convert(PizzaViewModel model)
        {
            if (model == null) return null;
            return new PizzaModel
            {
                Id = model.Id,
                PizzaName = model.PizzaName,
                Price = model.Price
            };
        }
    }
}