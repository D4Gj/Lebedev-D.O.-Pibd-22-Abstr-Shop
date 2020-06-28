using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PizzaShopBusinessLogic.BindingModels
{
    
    public class ClientBindingModel
    {
        
        public int? Id { set; get; }

       
        public string FIO { set; get; }

       
        public string Login { set; get; }

        
        public string Password { set; get; }
    }
}
