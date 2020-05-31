using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using PizzaShopBusinessLogic.Enums;
using PizzaShopFileImplement.Models;

namespace PizzaShopFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string IngridientFileName = "Ingridient.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PizzaFileName = "Pizza.xml";
        private readonly string PizzaIngridientFileName = "PizzaIngridient.xml";
        private readonly string StorageFileName = "Storage.xml";
        private readonly string StorageIngridientFileName = "StorageIngridient.xml";
        public List<Ingridient> Ingridients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizza { get; set; }
        public List<PizzaIngredient> PizzaIngridients { get; set; }
        public List<Storage> Storages { get; set; }
        public List<StorageIngridient> StorageIngridients { get; set; }
        private FileDataListSingleton()
        {
            Ingridients = LoadIngredients();
            Orders = LoadOrders();
            Pizza = LoadPizza();
            PizzaIngridients = LoadPizzaIngredients();
            Storages = LoadStorages();
            StorageIngridients = LoadStorageIngridients();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveIngredients();
            SaveOrders();
            SavePizza();
            SavePizzaIngredients();
            SaveStorages();
            SaveStorageIngridients();
        }
        private List<Ingridient> LoadIngredients()
        {
            var list = new List<Ingridient>();
            if (File.Exists(IngridientFileName))
            {
                XDocument xDocument = XDocument.Load(IngridientFileName);
                var xElements = xDocument.Root.Elements("Ingridient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Ingridient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngridientName = elem.Element("IngridientName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Pizza> LoadPizza()
        {
            var list = new List<Pizza>();
            if (File.Exists(PizzaFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaFileName);
                var xElements = xDocument.Root.Elements("Pizza").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Pizza
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaName = elem.Element("PizzaName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<PizzaIngredient> LoadPizzaIngredients()
        {
            var list = new List<PizzaIngredient>();
            if (File.Exists(PizzaIngridientFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaIngridientFileName);
                var xElements = xDocument.Root.Elements("PizzaIngridient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new PizzaIngredient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                        IngridientId = Convert.ToInt32(elem.Element("IngridientId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<Storage> LoadStorages()
        {
            var list = new List<Storage>();
            if (File.Exists(StorageFileName))
            {
                XDocument xDocument = XDocument.Load(StorageFileName);
                var xElements = xDocument.Root.Elements("Storage").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Storage()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StorageName = elem.Element("StorageName").Value.ToString()
                    });
                }
            }
            return list;
        }
        private List<StorageIngridient> LoadStorageIngridients()
        {
            var list = new List<StorageIngridient>();
            if (File.Exists(StorageIngridientFileName))
            {
                XDocument xDocument = XDocument.Load(StorageIngridientFileName);
                var xElements = xDocument.Root.Elements("StorageIngridient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new StorageIngridient()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngridientId = Convert.ToInt32(elem.Element("IngridientId").Value),
                        StorageId = Convert.ToInt32(elem.Element("StorageId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }

        private void SaveIngredients()
        {
            if (Ingridients != null)
            {
                var xElement = new XElement("Ingridients");
                foreach (var component in Ingridients)
                {
                    xElement.Add(new XElement("Ingridient",
                    new XAttribute("Id", component.Id),
                    new XElement("IngridientName", component.IngridientName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(IngridientFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("PizzaId", order.PizzaId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SavePizza()
        {
            if (Pizza != null)
            {
                var xElement = new XElement("Pizza");
                foreach (var product in Pizza)
                {
                    xElement.Add(new XElement("Pizza",
                    new XAttribute("Id", product.Id),
                    new XElement("PizzaName", product.PizzaName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(PizzaFileName);
            }
        }
        private void SavePizzaIngredients()
        {
            if (PizzaIngridients != null)
            {
                var xElement = new XElement("PizzaIngridients");
                foreach (var productComponent in PizzaIngridients)
                {
                    xElement.Add(new XElement("PizzaIngridient",
                    new XAttribute("Id", productComponent.Id),
                    new XElement("PizzaId", productComponent.PizzaId),
                    new XElement("IngridientId", productComponent.IngridientId),
                    new XElement("Count", productComponent.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(PizzaIngridientFileName);
            }
        }
        private void SaveStorages()
        {
            if (Storages != null)
            {
                var xElement = new XElement("Storages");
                foreach (var elem in Storages)
                {
                    xElement.Add(new XElement("Storage",
                        new XAttribute("Id", elem.Id),
                        new XElement("StorageName", elem.StorageName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageFileName);
            }
        }
        private void SaveStorageIngridients()
        {
            if (StorageIngridients != null)
            {
                var xElement = new XElement("StorageIngridients");
                foreach (var elem in StorageIngridients)
                {
                    xElement.Add(new XElement("StorageIngridient",
                        new XAttribute("Id", elem.Id),
                        new XElement("IngridientId", elem.IngridientId),
                        new XElement("StorageId", elem.StorageId),
                        new XElement("Count", elem.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageIngridientFileName);
            }
        }
    }
}
