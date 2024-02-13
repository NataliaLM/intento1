using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace intento1.Models
{
    public class CarritoCompraModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CarritoCompra cc;
            cc = (CarritoCompra)controllerContext.HttpContext.Session["KEY_CC_123"];
            if (cc == null)
            {
                cc = new CarritoCompra();
                controllerContext.HttpContext.Session["KEY_CC_123"] = cc;
            }

            return cc;
        }
    }
}