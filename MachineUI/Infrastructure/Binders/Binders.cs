using Machine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MachineUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = new Cart();
            cart.Id = int.Parse(controllerContext.HttpContext.Request.Form["Id"]);
            cart.Quantity = int.Parse(controllerContext.HttpContext.Request.Form["Quantity"]);
            return cart;
        }
    }
}