using Machine.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Machine.Domain.Entities;
using System.Xml;
using System.Xml.Linq;

namespace Machine.Domain.Concrete
{
    public class DrinkXmlRepository : IDrinkRepository
    {      

        public IEnumerable<Drink> GetDrinks()
        {
             List<Drink> list = new List<Drink>();
             XmlDocument xmlDoc = new XmlDocument();
             var path = HttpContext.Current.Request.MapPath("~/Drinks.xml");
             xmlDoc.Load(path);
             foreach (XmlNode node in xmlDoc.SelectNodes("drinks/drink"))
             {
                 Drink drink = new Drink();
                 drink.Id = Convert.ToInt16(node.SelectSingleNode("id").InnerText);
                 drink.Name = node.SelectSingleNode("name").InnerText;
                 drink.Price = Convert.ToInt16(node.SelectSingleNode("price").InnerText);
                 drink.Quantity = Convert.ToInt16(node.SelectSingleNode("quantity").InnerText);
                 list.Add(drink);
             }                     
            return list;
        }

        public void Update(Cart cart)
        {
            XmlDocument xmlDoc = new XmlDocument();
            var path = HttpContext.Current.Request.MapPath("~/Drinks.xml");
            xmlDoc.Load(path);
            foreach (XmlNode node in xmlDoc.SelectNodes("drinks/drink"))
            {
                if (cart.Id == Convert.ToInt16(node.SelectSingleNode("id").InnerText))
                {
                    int quant = Convert.ToInt16(node.SelectSingleNode("quantity").InnerText) - cart.Quantity;
                    node.SelectSingleNode("quantity").InnerText = quant.ToString();
                    break;
                }
            }
            xmlDoc.Save(path);
        }

        ~DrinkXmlRepository()
        {

        }
    }
}