using LarnerhemsEvent.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LarnerhemsEvent.Controllers
{
    public class AdminController : Controller
    {
        private DbOp dbc = new DbOp();
        // GET: Admin
        public ActionResult Index()
        {
            if(TempData["admin"] == "true")
            {
                TempData["admin"] = "true";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Admin");
            }
            
        }
        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];

            bool login = dbc.Login(username, password);
            if(login == true)
            {
                TempData["admin"] = "true";
                TempData["felinlogg"] = "";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["admin"] = "false";
                TempData["felinlogg"] = "Felaktiga inloggningsuppgifter!";
                return View(Login());
            }
            
        }
    }
}