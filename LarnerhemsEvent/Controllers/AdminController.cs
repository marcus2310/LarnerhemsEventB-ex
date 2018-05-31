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
            if(TempData["admin"] == "true")
            {
                TempData["admin"] = "true";
                var camps = dbc.GetAllCampaignCodes();

                return View(camps);

            }
            else
            {

                return RedirectToAction("Login", "Admin");
            }

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

            TempData["admin"] = "true";
            return RedirectToAction("Kampanjkod", "Admin");
        }
        public ActionResult Anvandare()
        {
            if(TempData["admin"] == "true")
            {
                TempData["admin"] = "true";
            var userList = dbc.GetAllusers();

            return View(userList);


            }
            else
            {

                return RedirectToAction("Login", "Admin");
            }



        }
        [HttpPost]
        public ActionResult Anvandare(FormCollection form)
        {
            var userID = form["taBortUser"];
            var username = form["usernameUser"];
            var password = form["passwordUser"];

            if(userID != null)
            {
                dbc.DeleteUser(Convert.ToInt32(userID));

            }
            else if (username != "" && password != "")
            {
                dbc.CreateUser(username, password, 2);

            }

            TempData["admin"] = "true";
            return RedirectToAction("Anvandare", "Admin");

        }
        public ActionResult Paket()
        {
            if(TempData["admin"] == "true")
            {
                TempData["admin"] = "true";
            var packList = dbc.GetAllPackages();

            return View(packList);


            
            }
            else
            {

                return RedirectToAction("Login", "Admin");
            }


        }
        public ActionResult Redigera(int id)
        {
            if(TempData["admin"] == "true")
            {
                TempData["admin"] = "true";
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
            else
            {
                return RedirectToAction("Login", "Admin");
            }
  
        }
        [HttpPost]
        public ActionResult Redigera(FormCollection form)
        {
            try
            {
                var packageid = form["packageid"];
                var name = form["name"];
                var price = form["price"];
                var originalprice = form["originalprice"];
                var info = form["info"];
                var moreinfo = form["moreinfo"];

                dbc.UpdatePackage(Convert.ToInt32(packageid), name, Convert.ToInt32(price), Convert.ToInt32(originalprice), info, moreinfo);

                TempData["admin"] = "true";
                return RedirectToAction("Redigera", new { id = Convert.ToInt32(packageid)});
            }
            catch (Exception)
            {
                return RedirectToAction("Paket", "Admin");

            }

        }
        public ActionResult Orders()
        {
            try
            {
                var OrderList = dbc.GetAllOrders();

                return View(OrderList);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        [HttpPost]
        public ActionResult Orders(FormCollection form)
        {
            try
            {
                
                var orderID = form["approveorder"];
                var tabortorderID = form["tabortorder"];

                if(orderID != null)
                {
                    var order = dbc.GetOrder(Convert.ToInt32(orderID));
                    order.approved = "true";
                    dbc.UpdateOrder(order);

                }
                if(tabortorderID != null)
                {
                    dbc.DeleteOrder(Convert.ToInt32(tabortorderID));

                }

                return RedirectToAction("Orders", "Admin");
            }
            catch (Exception)
            {
                return RedirectToAction("Orders", "Admin");
            }
        }
        public ActionResult Orderinfo(int id)
        {
            try
            {
                var order = dbc.GetOrder(id);

                return View(order);
            }
            catch (Exception)
            {
                return RedirectToAction("Orders", "Admin");
            }

        }
    }
}