using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["CurrentState"] == null)
            {
                Village v = new Village();

                v.Day = 1;
                v.Villagers = 1;
                v.Wood = 5;
                v.Food = 6;
                v.Water = 6;
                v.Wells = 0;
                v.ActionsLeft = v.Day; 

                Session["CurrentState"] = v;
                ViewBag.CurrentState = v;
            }
            return View();
        }

        public ActionResult BuildHouse()
        {
            Village v = (Village)Session["CurrentState"];
            v.Wood -= 5;
            v.Villagers++;

            Session["CurrentState"] = v;
            ViewBag.CurrentState = v;

            return Index();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}