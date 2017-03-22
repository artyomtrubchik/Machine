using Machine.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Machine.Domain.Entities;
using Microsoft.Practices.Unity;
using System.Xml;
using System.Security.Policy;
using System.IO;

namespace Machine.Domain.Concrete
{
    public class CoinXmlRepository : ICoinRepository
    {
        public IEnumerable<Coin> GetCoins()
        {
            List<Coin> list = new List<Coin>();
            XmlDocument xmlDoc = new XmlDocument();
            var path = HttpContext.Current.Request.MapPath("~/Coins.xml");
            xmlDoc.Load(path);
            foreach (XmlNode node in xmlDoc.SelectNodes("coins/coin"))
            {
                Coin coin = new Coin();
                coin.Id = Convert.ToInt16(node.SelectSingleNode("id").InnerText);
                coin.Value = Convert.ToInt16(node.SelectSingleNode("value").InnerText);
                coin.IsLocked = Convert.ToBoolean(node.SelectSingleNode("islocked").InnerText);
                list.Add(coin);
            }
            return list;
        }

       

        ~CoinXmlRepository()
        {

        }

       
    }
}