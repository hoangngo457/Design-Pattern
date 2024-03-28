using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vieon.Controllers
{
    public abstract class TemplateMethodController : Controller
    {
        protected abstract void PrintRoutes();
        protected abstract void PrintDIs(); //In ra các Dependencies Injecttion

        //Template Method

        public void PrintInformation()
        {
            PrintRoutes();
            PrintDIs();
        }
    }
}