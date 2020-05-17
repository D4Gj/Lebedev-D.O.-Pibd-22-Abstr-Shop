using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace PizzaShopBusinessLogic.ViewModels
{
    public class IngridientViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название ингредиента")]
        public string IngridientName { get; set; }
    }
}
