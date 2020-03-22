using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaShopBusinessLogic.ViewModels
{
    public class StorageViewModel
    {
        public int Id { set; get; }
        [DisplayName("Хранилище")]
        public string StorageName { set; get; }
        public Dictionary<int, (string, int)> StoragedMaterials { get; set; }
    }
}
