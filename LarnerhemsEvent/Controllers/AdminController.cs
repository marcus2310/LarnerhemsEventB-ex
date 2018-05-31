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
        public ActionResult Kampanjkod()
        {
           
                var camps = dbc.GetAllCampaignCodes();

                return View(camps);

          

        }
        [HttpPost]
        public ActionResult Kampanjkod(FormCollection form)
        {
            var campID = form["taBortCamp"];
            var campCode = form["campCode"];
            var rabatt = form["rabatt"];

            if(campID != null)
            {
                dbc.DeleteCampaign(Convert.ToInt32(campID));
            }
            else if(campCode != "" && rabatt != "")
            {
                dbc.CreateCampaignCode(campCode, Convert.ToInt32(rabatt));
            }
        

            return RedirectToAction("Kampanjkod", "Admin");
        }
        public ActionResult Anvandare()
        {

            var userList = dbc.GetAllusers();

            return View(userList);
        }
        [HttpPost]
        public ActionResult Anvandare(FormCollection form)
        {
            var userID = form["taBortUser"];
            var username = form["usernameUser"];
            var password = form["passwordUser"];

            if (userID != null)
            {
                dbc.DeleteUser(Convert.ToInt32(userID));

            }
            else if (username != "" && password != "")
            {
                dbc.CreateUser(username, password, 2);

            }


            return RedirectToAction("Anvandare", "Admin");

        }
        public ActionResult Paket()
        {
            var packList = dbc.GetAllPackages();

            return View(packList);
        }
        public ActionResult Redigera(int id)
        {
            try
            {
                var package = dbc.GetAPackage(id);
                return View(package);

            }
            catch (Exception)
            {
                return RedirectToAction("Paket","Admin");
            }
         
        }
    }
}