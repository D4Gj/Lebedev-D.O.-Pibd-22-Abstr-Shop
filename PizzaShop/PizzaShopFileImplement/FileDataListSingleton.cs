﻿using System;
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
        private readonly string IngredientFileName = "Ingridient.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PizzaFileName = "Pizzas.xml";
        private readonly string PizzaIngredientFileName = "PizzaIngridient.xml";
        private readonly string ClientFileName = "Client.xml";
        public List<Ingridient> Ingridients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public List<PizzaIngredient> PizzaIngridients { get; set; }
        public List<Client> Clients { get; set; }
        private FileDataListSingleton()
        {
            Ingridients = LoadIngredients();
            Orders = LoadOrders();
            Pizzas = LoadPizza();
            PizzaIngridients = LoadPizzaIngredients();
            Clients = LoadClients();
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
            SaveClients();
        }
        private List<Ingridient> LoadIngredients()
        {
            var list = new List<Ingridient>();
            if (File.Exists(IngredientFileName))
            {
                XDocument xDocument = XDocument.Load(IngredientFileName);
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
                var xElements = xDocument.Root.Elements("Pizzas").ToList();
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
            if (File.Exists(PizzaIngredientFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaIngredientFileName);
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
                xDocument.Save(IngredientFileName);
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
            if (Pizzas != null)
            {
                var xElement = new XElement("Pizzas");
                foreach (var product in Pizzas)
                {
                    xElement.Add(new XElement("Pizzas",
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
                xDocument.Save(PizzaIngredientFileName);
            }
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }

            return list;
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("FIO", client.FIO),
                    new XElement("Login", client.Login),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
    }
}
