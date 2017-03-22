using Machine.Domain.Abstract;
using Machine.Domain.Entities;
using MachineUI.Models;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineUI.Controllers
{
    public class HomeController : Controller
    {
        private ICoinRepository coinRepository;
        private IDrinkRepository drinkRepository;
        private IUnityContainer container;
        private int balance = 0;
        private string sessionKey = "balance";

        public HomeController(ICoinRepository _coinRepository, IDrinkRepository _drinkRepository, IUnityContainer _container)
        {
            coinRepository = _coinRepository;
            drinkRepository = _drinkRepository;
            container = _container;
        }

      
        public ActionResult Index()
        {
            balance = HttpContext.Session[sessionKey] != null ? (int)HttpContext.Session[sessionKey] : 0;
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            HomeViewModel model = new HomeViewModel
            {
                Coins = coinRepository.GetCoins(),
                Drinks = drinkRepository.GetDrinks(),
                Balance = balance
            };            
            return View(model);
        }

        [HttpPost]       
        public ActionResult Index([Bind(Include = "Balance")] HomeViewModel model, bool getChange = false)
        {
            if (getChange == true) HttpContext.Session[sessionKey] = 0;
            balance = HttpContext.Session[sessionKey] != null ? (int)HttpContext.Session[sessionKey] : 0;
            if (ModelState.IsValid)
            {
                balance += model.Balance;
                HttpContext.Session[sessionKey] = balance;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }           
        }


        public ActionResult AfterShopping()
        {
            HomeViewModel model = new HomeViewModel
            {
                Coins = coinRepository.GetCoins()
                    .OrderBy(x => x.Value),
                Drinks = drinkRepository.GetDrinks()
                    .Where(x => (x.Quantity>0))
                    .OrderBy(x => x.Price),                    
                Balance = HttpContext.Session[sessionKey] != null ? (int)HttpContext.Session[sessionKey] : 0
            };
            return View("~/Views/Home/Index.cshtml", model);
        }

      
        [HttpPost]
        public ActionResult Cart(Cart cart)
        {
            drinkRepository.Update(cart);
            int price = drinkRepository.GetDrinks().FirstOrDefault(g => g.Id == cart.Id).Price;
            balance = (int)HttpContext.Session[sessionKey];
            balance -= price;
            HttpContext.Session[sessionKey] = balance;
            return RedirectToAction("AfterShopping");
        }

        [HttpPost]
        public ActionResult ChangeSource(string source)
        {
            UnityConfigurationSection section
                   = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if (source == "sql") container.LoadConfiguration(section, "sql");
            else container.LoadConfiguration(section, "xml");
            return RedirectToAction("Index");
        }



    }
}