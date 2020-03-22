using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShopBusinessLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int? Id { set; get; }
        public string StorageName { set; get; }
        //materialId , (name, count)
        public Dictionary<int, (string, int)> StoragedMaterials { get; set; }
    }
}
