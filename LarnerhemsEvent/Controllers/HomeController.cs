using LarnerhemsEvent.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LarnerhemsEvent.Controllers
{
    public class HomeController : Controller
    {
        private DbOp dbc = new DbOp();
        public ActionResult Index()
        {
            dbc.DeleteUnfinnishedOrder();
            TempData["klar"] = "";
            TempData["admin"] = "false";

            return View();
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
        public ActionResult Boka()
        {

            return View();
        }
        public ActionResult Error()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}