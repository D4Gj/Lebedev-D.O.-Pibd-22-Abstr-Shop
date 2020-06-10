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
        public List<ReportPizzaOrdersViewModel> GetPizzaIngridient()
        {
            var pizzas = pizzaShopLogic.Read(null);
            var list = new List<ReportPizzaOrdersViewModel>();
            foreach (var pizza in pizzas)
            {
                foreach (var pizzIngr in pizza.PizzaIngridients)
                {
                        var record = new ReportPizzaOrdersViewModel
                        {                            
                            PizzaName = pizza.PizzaName,
                            IngridientName = pizzIngr.Value.Item1,
                            Count = pizzIngr.Value.Item2
                        };
                        list.Add(record);
                }
            }
            return list;
        }

        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
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
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                Pizzas = GetPizzaIngridient()            
            });
        }
    }
}
