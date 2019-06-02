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

        Random r = new Random();

        public ActionResult Index()
        {
            if(Session["CurrentState"] == null)
            {
                Village v = new Village();

                v.Day = 1;
                v.Villagers = 1;
                v.Wood = 0;
                v.Food = 1;
                v.Water = 1;
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

            return View("Index");
        }

        public ActionResult BuildWell()
        {
            Village v = (Village)Session["CurrentState"];
            v.Wood -= 5;
            v.Wells++;

            Session["CurrentState"] = v;
            ViewBag.CurrentState = v;

            return View("Index");
        }

        public ActionResult GatherFood()
        {
            Village v = (Village)Session["CurrentState"];

            int gatheredFood = r.Next(0, 5);
            v.Food += gatheredFood;
            v.Food--;
            v.Water--;
            v.ActionsLeft--;

            if(v.Water == 0 || v.Food == 0)
            {
                return RedirectToAction("Fail");
            }

            if (v.ActionsLeft == 0)
            {
                v.ActionsLeft = v.Villagers;
                v.Day++;
            }

            Session["CurrentState"] = v;
            ViewBag.CurrentState = v;
            ViewBag.Message = $"You went to gather food and got {gatheredFood} units for your village.";

            return View("Index");
        }

        public ActionResult GatherWater()
        {
            Village v = (Village)Session["CurrentState"];

            int gatheredWater = r.Next(0, 5);
            v.Water += gatheredWater;
            v.Food--;
            v.Water--;
            v.ActionsLeft--;

            if (v.Water == 0 || v.Food == 0)
            {
                return RedirectToAction("Fail");
            }

            if (v.ActionsLeft == 0)
            {
                RedirectToAction("NextDay");
            }

            Session["CurrentState"] = v;
            ViewBag.CurrentState = v;
            ViewBag.Message = $"You went to gather food and got {gatheredWater} units for your village.";

            return View("Index");
        }


        public ActionResult Fail()
        {
            Village v = (Village)Session["CurrentState"];

            ViewBag.Message = $"You've run out of supplies and your villager{(v.Villagers == 1 ? "" : "s")} {(v.Villagers == 1 ? "has" : "have")} died.";
            v.Villagers = 0;
            v.ActionsLeft = 0;

            Session["CurrentState"] = v;
            ViewBag.CurrentState = v;

            return View("Index");

        }

        public ActionResult NextDay()
        {
            Village v = (Village)Session["CurrentState"];

            ViewBag.Message = "You've completed the day.";
            v.Day++;

            Session["CurrentState"] = v;
            ViewBag.CurrentState = v;

            return View("Index");
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