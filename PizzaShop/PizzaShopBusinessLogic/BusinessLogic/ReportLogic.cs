using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.BindingModels;
using PizzaShopBusinessLogic.HelperModels;
using PizzaShopBusinessLogic.Interfaces;
using PizzaShopBusinessLogic.ViewModels;
using System.Linq;

namespace PizzaShopBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IIngridientLogic ingridientLogic;
        private readonly IPizzaShopLogic pizzaShopLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IPizzaShopLogic pizzaShopLogic, IIngridientLogic ingridientLogic,
       IOrderLogic orderLogic)
        {
            this.pizzaShopLogic = pizzaShopLogic;
            this.ingridientLogic = ingridientLogic;
            this.orderLogic = orderLogic;
        }
        public List<ReportPizzaIngridientViewModel> GetProductComponent()
        {
            var components = ingridientLogic.Read(null);
            var products = pizzaShopLogic.Read(null);
            var list = new List<ReportPizzaIngridientViewModel>();
            foreach (var product in products)
            {
                var record = new ReportPizzaIngridientViewModel
                {
                    PizzaName = product.PizzaName,
                    Pizzas = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (product.PizzaIngridients.ContainsKey(component.Id))
                    {
                        record.Pizzas.Add(new Tuple<string, int>(component.IngridientName,
                       product.PizzaIngridients[component.Id].Item2));
                        record.TotalCount +=
                       product.PizzaIngridients[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

                
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                PizzaName = x.PizzaName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                Pizzas = pizzaShopLogic.Read(null),

            });
        }

        public void SaveProductComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                Pizzas = pizzaShopLogic.Read(null)
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
