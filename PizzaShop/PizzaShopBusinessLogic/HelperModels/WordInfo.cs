﻿using System;
using System.Collections.Generic;
using System.Text;
using PizzaShopBusinessLogic.ViewModels;

namespace PizzaShopBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<PizzaViewModel> Pizzas { get; set; }
        public List<StorageViewModel> Storages { get; set; }
    }
}
