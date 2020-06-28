using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzaShopBusinessLogic.Attributes;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class IngridientViewModel : BaseViewModel
    {
        [Column(title: "Ингридиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngridientName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "IngridientName"
        };
    }
}
