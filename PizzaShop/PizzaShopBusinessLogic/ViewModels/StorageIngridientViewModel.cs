using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class StorageIngridientViewModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int IngridientId { get; set; }

        [DisplayName("Ингридиент")]
        public string IngridientName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
