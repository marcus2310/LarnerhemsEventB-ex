using LarnerhemsEvent.DBOperations;
using LarnerhemsEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LarnerhemsEvent.Models.ViewModels;
using System.Web.Security;
using LarnerhemsEvent.E_mail;
using System.Net.Mail;

namespace LarnerhemsEvent.Controllers
{
    public class BokaController : Controller
    {
        private DbOp dbc = new DbOp();
        List<package> tentpackList = new List<package>();
        List<package> floorPackList = new List<package>();
        List<package> allPackList = new List<package>();
        List<package> soundPackList = new List<package>();
        List<package> LightPackList = new List<package>();
        List<package> ExtraPackList = new List<package>();
        List<package> ChoosenPackages = new List<package>();

        VMPackages Packages = new VMPackages();

        // GET: Boka
        public ActionResult Index()
        {
            HttpCookie cookie = new HttpCookie("OrderIDCookie");

            cookie.Expires = DateTime.Now.AddHours(5);
            HttpContext.Response.SetCookie(cookie);

            TempData["summa"] = "0";
            tentpackList = dbc.GetTentPackages();

            return View(tentpackList);
        }
       
       
        [HttpPost]
      
        public ActionResult Index(FormCollection form)
        {

            try
            {
                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);


                if (Newcookie.Value == "")
                {
                    var orderIDt = dbc.CreateOrder();
                    Newcookie.Value = orderIDt.ToString();
                    HttpContext.Response.SetCookie(Newcookie);
                }

             
                var PackageId = form["buttonItem"];
                
                int itemID = Convert.ToInt32(PackageId);
                int orderID = Convert.ToInt32(Newcookie.Value);
                var item = dbc.GetAPackage(itemID);
                int totalPrice;
                //sätter värden på ordern
                TempData["tentItem"] = item.packageID;
                dbc.AddToPackOrderDetail(orderID, itemID, 1);

                totalPrice = dbc.GetTotalPrice(orderID);
                var order = dbc.GetOrder(orderID);
                dbc.SetTotalpriceOrder(orderID, totalPrice);
                TempData["summa"] = order.totalprice;


                
               
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
          
            TempData["Auth"] = "steg1";

            return RedirectToAction("Golv", "Boka");
        }
        
        public ActionResult Golv()
        {
            try
            {
                if(TempData["Auth"].ToString() == "steg1"|| TempData["Auth"].ToString() == "steg2" || TempData["Auth"].ToString() == "steg3")
                {
                    floorPackList = dbc.GetFloorPackages();
                    
                    return View(floorPackList);
                }
                else
                {
                    TempData["auth"] = null;
                    return RedirectToAction("Index", "Boka");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");

            }
            
        }
        [HttpPost]
        public ActionResult Golv(FormCollection form)
        {
            try
            {
                
                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);


                if (Newcookie.Value == "")
                {
                    return RedirectToAction("Index", "Boka");
                }
           

                var PackageId = form["buttonItem"];

                int itemID = Convert.ToInt32(PackageId);
                int orderID = Convert.ToInt32(Newcookie.Value);
                var item = dbc.GetAPackage(itemID);
                int totalPrice;

                var tillbaka = form["tillbaka"];
                var fram = form["fram"];

                if (tillbaka != null)
                {
                    //här kan vi sedan ta bort paket som valts
                    dbc.DeleteSelectedPackage(orderID, 1);
                    TempData["auth"] = "steg2";
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    return RedirectToAction("Index", "Boka");

                }
                if(fram != null)
                {
                    //går framåt utan paket.
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    TempData["Auth"] = "steg2";
                    return RedirectToAction("Ljud", "Boka");


                }

                //sätter värden på ordern
                TempData["floorItem"] = item.packageID;
                dbc.AddToPackOrderDetail(orderID, itemID, 1);

                var order = dbc.GetOrder(orderID);
                totalPrice = dbc.GetTotalPrice(orderID);
                dbc.SetTotalpriceOrder(orderID, totalPrice);
                TempData["summa"] = totalPrice;



        

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }


            TempData["Auth"] = "steg2";
            return RedirectToAction("Ljud", "Boka");
        }

        public ActionResult Ljud()
        {
            try
            {
                if (TempData["Auth"].ToString() == "steg2" || TempData["Auth"].ToString() == "steg3" || TempData["Auth"].ToString() == "steg4")
                {
                    soundPackList = dbc.GetSoundPackages();

                    return View(soundPackList);

                }
                else
                {
                    return RedirectToAction("Index", "Boka");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");

            }
            
        }
        [HttpPost]
        public ActionResult Ljud(FormCollection form)
        {
            try
            {
                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);


                if (Newcookie.Value == "")
                {
                    return RedirectToAction("Index", "Boka");
                }


                var PackageId = form["buttonItem"];

                int itemID = Convert.ToInt32(PackageId);
                int orderID = Convert.ToInt32(Newcookie.Value);
                var item = dbc.GetAPackage(itemID);

                int totalPrice;

                var tillbaka = form["tillbaka"];
                var fram = form["fram"];
                if (tillbaka != null)
                {
                    //här kan vi sedan ta bort paket som valts
                    dbc.DeleteSelectedPackage(orderID, 2);
                    TempData["auth"] = "steg2";
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    return RedirectToAction("Golv", "Boka");

                }
                if (fram != null)
                {
                    //går framåt utan paket.
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    TempData["Auth"] = "steg3";
                    return RedirectToAction("Ljus", "Boka");


                }
                //sätter värden på ordern
                TempData["soundItem"] = item.packageID;
                dbc.AddToPackOrderDetail(orderID, itemID, 1);

                var order = dbc.GetOrder(orderID);
                totalPrice = dbc.GetTotalPrice(orderID);
                dbc.SetTotalpriceOrder(orderID, totalPrice);
                TempData["summa"] = totalPrice;

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }


            TempData["Auth"] = "steg3";
            return RedirectToAction("Ljus", "Boka");
        }
       
        public ActionResult Ljus()
        {
            try
            {
                if (TempData["Auth"].ToString() == "steg3" || TempData["Auth"].ToString() == "steg4" || TempData["Auth"].ToString() == "steg5")
                {
                    LightPackList = dbc.GetLightPackages();

                    return View(LightPackList);

                }
                else
                {
                    return RedirectToAction("Index", "Boka");
                }


            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");

            }
           
        }
        [HttpPost]
        public ActionResult Ljus(FormCollection form)
        {
            try
            {
                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);


                if (Newcookie.Value == "")
                {
                    return RedirectToAction("Index", "Boka");
                }


                var PackageId = form["buttonItem"];

                int itemID = Convert.ToInt32(PackageId);
                int orderID = Convert.ToInt32(Newcookie.Value);
                var item = dbc.GetAPackage(itemID);
                int totalPrice;

                var tillbaka = form["tillbaka"];
                var fram = form["fram"];
                if (tillbaka != null)
                {
                    //här kan vi sedan ta bort paket som valts
                    dbc.DeleteSelectedPackage(orderID, 3);
                    TempData["auth"] = "steg3";
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    return RedirectToAction("Ljud", "Boka");

                }
                if (fram != null)
                {
                    //går framåt utan paket.
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    TempData["Auth"] = "steg4";
                    return RedirectToAction("Tillbehor", "Boka");


                }
                //sätter värden på ordern
                TempData["lightItem"] = item.packageID;
                dbc.AddToPackOrderDetail(orderID, itemID, 1);

                var order = dbc.GetOrder(orderID);
                totalPrice = dbc.GetTotalPrice(orderID);
                dbc.SetTotalpriceOrder(orderID, totalPrice);
                TempData["summa"] = totalPrice;

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");

            }

            TempData["Auth"] = "steg4";
            return RedirectToAction("Tillbehor", "Boka");
        }

        public ActionResult Tillbehor()
        {
            try
            {
                if (TempData["Auth"].ToString() == "steg4" || TempData["Auth"].ToString() == "steg5" || TempData["Auth"].ToString() == "steg6")
                {
                    ExtraPackList = dbc.GetExtraPackages();

                    return View(ExtraPackList);

                }
                else
                {
                    return RedirectToAction("Index", "Boka");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public ActionResult Tillbehor(FormCollection form)
        {
            try
            {
                var PackageValue1 = form["13"];
                var PackageValue2 = form["14"];
                var PackageValue3 = form["15"];
                var PackageValue4 = form["16"];
                var PackageValue5 = form["17"];
                var PackageValue6 = form["18"];

                var item1 = dbc.GetAPackage(13);
                var item2 = dbc.GetAPackage(14);
                var item3 = dbc.GetAPackage(15);
                var item4 = dbc.GetAPackage(16);
                var item5 = dbc.GetAPackage(17);
                var item6 = dbc.GetAPackage(18);

                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);
                

                if (Newcookie.Value == "")
                {
                    return RedirectToAction("Index", "Boka");
                }
                else
                {
                    int orderID = Convert.ToInt32(Newcookie.Value);
                    var order = dbc.GetOrder(orderID);
                    var tillbaka = form["tillbaka"];

                    if (tillbaka != null)
                    {
                        //här kan vi sedan ta bort paket som valts
                        int totalPrice;
                        dbc.DeleteSelectedPackage(orderID, 4);
                        TempData["auth"] = "steg4";
                        totalPrice = dbc.GetTotalPrice(orderID);
                        TempData["summa"] = totalPrice;
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        return RedirectToAction("Ljus", "Boka");

                    }

                   

                    if (PackageValue1 != "" && PackageValue1 != "0" && PackageValue1 != "00")
                    {
                        int totalPrice;
                        int itemAmount1 = Convert.ToInt32(PackageValue1);
                        TempData["TillbehorItem1"] = item1.packageID;
                        TempData["TillbehorAmount1"] = itemAmount1;

                        //sätter värden på ordern
                        dbc.AddToPackOrderDetail(orderID, item1.packageID, itemAmount1);
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;

                    }
                    if (PackageValue2 != "" && PackageValue2 != "0" && PackageValue2 != "00")
                    {
                        int totalPrice;
                        int itemAmount2 = Convert.ToInt32(PackageValue2);
                        TempData["TillbehorItem2"] = item2.packageID;
                        TempData["TillbehorAmount2"] = itemAmount2;

                        //sätter värden på ordern
                        dbc.AddToPackOrderDetail(orderID, item2.packageID, itemAmount2);
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;
                    }
                    if (PackageValue3 != "" && PackageValue3 != "0" && PackageValue3 != "00")
                    {
                        int totalPrice;
                        int itemAmount3 = Convert.ToInt32(PackageValue3);
                        TempData["TillbehorItem3"] = item3.packageID;
                        TempData["TillbehorAmount3"] = itemAmount3;

                        //sätter värden på ordern
                        dbc.AddToPackOrderDetail(orderID, item3.packageID, itemAmount3);
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;
                    }
                    if (PackageValue4 != "" && PackageValue4 != "0" && PackageValue4 != "00")
                    {
                        int totalPrice;
                        int itemAmount4 = Convert.ToInt32(PackageValue4);
                        TempData["TillbehorItem4"] = item4.packageID;
                        TempData["TillbehorAmount4"] = itemAmount4;

                        //sätter värden på ordern
                        dbc.AddToPackOrderDetail(orderID, item4.packageID, itemAmount4);
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;
                    }
                    if (PackageValue5 != "" && PackageValue5 != "0" && PackageValue5 != "00")
                    {
                        int totalPrice;
                        int itemAmount5 = Convert.ToInt32(PackageValue5);
                        TempData["TillbehorItem5"] = item5.packageID;
                        TempData["TillbehorAmount5"] = itemAmount5;

                        //sätter värden på ordern
                        dbc.AddToPackOrderDetail(orderID, item5.packageID, itemAmount5);
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;
                    }
                    if (PackageValue6 != "" && PackageValue6 != "0" && PackageValue6 != "00")
                    {
                        int totalPrice;
                        int itemAmount6 = Convert.ToInt32(PackageValue6);
                        TempData["TillbehorItem6"] = item6.packageID;
                        TempData["TillbehorAmount6"] = itemAmount6;

                        //sätter värden på ordern
                        dbc.AddToPackOrderDetail(orderID, item6.packageID, itemAmount6);
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;
                    }
                    else
                    {
                        int totalPrice;
                        totalPrice = dbc.GetTotalPrice(orderID);
                        dbc.SetTotalpriceOrder(orderID, totalPrice);
                        TempData["summa"] = totalPrice;
                    }


                }

            }
            catch (Exception)
            {
                TempData["Auth"] = "steg1";
                return RedirectToAction("Error", "Home");

            }

            TempData["Auth"] = "steg5";
            return RedirectToAction("Slutfor", "Boka");
        }


        public ActionResult Slutfor()
        {
          
            List<VMPackOrder> VMpackList = new List<VMPackOrder>();
            List<package> selectedPackList = new List<package>();
            List<int> PackageIDList = new List<int>();
            
            try
            {
                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);
                int orderIDcookie = Convert.ToInt32(Newcookie.Value);
                if (TempData["Auth"].ToString() == "steg5" || TempData["Auth"].ToString() == "steg6")
                {
                    var packorderList = dbc.getOrderdetails(orderIDcookie);
                    foreach (var i in packorderList)
                    {
                        PackageIDList.Add(i.fk_package_id);

                    }
                    //PackageIDList.Add(Convert.ToInt32(tempItem));
                    selectedPackList = dbc.GetSelectedPackages(PackageIDList);

                    foreach (var p in selectedPackList)
                    {
                        foreach (var o in packorderList)
                        {
                            if(p.packageID == o.fk_package_id)
                            {
                                VMPackOrder VMpackObject = new VMPackOrder();

                                VMpackObject.VMPackages = p;
                                VMpackObject.antal = Convert.ToInt32(o.amount);
                                VMpackList.Add(VMpackObject);

                                break;
                            }
                            else
                            {

                            }

                        }

                    }

            

                    return View(VMpackList);
                }
                else
                {
                    return RedirectToAction("Index", "Boka");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public ActionResult Slutfor(FormCollection form)
        {
            try
            {
                var firstname = form["firstname"];
                var lastname = form["lastname"];
                var telenumber = form["telenumber"];
                var email = form["email"];

                var address = form["address"];
                var town = form["town"];
                var zipcode = form["zipcode"];
                var date = form["date"];
                var moreinfo = form["moreinfo"];
                var kampanjkod = form["kampanjkod"];

                var taBortID = form["taBortPack"];

                HttpCookie Newcookie = Request.Cookies["OrderIDCookie"];
                Newcookie.Expires = DateTime.Now.AddHours(5);


                if (Newcookie.Value == "")
                {
                    return RedirectToAction("Index", "Boka");
                }
                int orderID = Convert.ToInt32(Newcookie.Value);

                var tillbaka = form["tillbaka"];
                if (tillbaka != null)
                {
                    //här kan vi sedan ta bort paket som valts
                    int totalPrice;
                    dbc.DeleteSelectedPackage(orderID, 5);
                    TempData["auth"] = "steg4";
                    totalPrice = dbc.GetTotalPrice(orderID);
                    TempData["summa"] = totalPrice;
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    return RedirectToAction("Tillbehor", "Boka");
                }
                else if (taBortID != null)
                {
                    dbc.DeleteSelectedPackageByID(orderID, Convert.ToInt32(taBortID));
                    int totalPrice;
                    totalPrice = dbc.GetTotalPrice(orderID);
                    dbc.SetTotalpriceOrder(orderID, totalPrice);
                    TempData["summa"] = totalPrice;
                    TempData["auth"] = "steg6";
                    return RedirectToAction("Slutfor", "Boka");
                }
                else
                {
                    //här ska det skrivas kod för att skapa HELA ORDERN!!

                    //skapar en kund!
                    customer cust = new customer();
                    cust.firstname = firstname;
                    cust.lastname = lastname;
                    cust.phonenumber = telenumber;
                    cust.email = email;
                    int customerID = dbc.CreateCustomer(cust);

                    var order = dbc.GetOrder(orderID);
                    order.requests = moreinfo;
                    order.deliveryadress = address;
                    order.zipcode = zipcode;
                    order.town = town;
                    order.eventdate = Convert.ToDateTime(date);
                    order.fk_customer_id = customerID;

                    if (kampanjkod != "")
                    {
                        var campaigncode = dbc.GetCampaigncode(kampanjkod);

                        if (campaigncode != null)
                        {
                            order.fk_campaigncode_id = campaigncode.campaigncodeID;
                            int rabatt = Convert.ToInt32(campaigncode.amount);
                            int totalPrice;
                            totalPrice = dbc.GetTotalPrice(orderID);
                            var sumAfterRabatt = totalPrice - rabatt;
                            order.totalprice = sumAfterRabatt;

                            TempData["summa"] = sumAfterRabatt;
                            TempData["campaigncodeText"] = "";

                            dbc.UpdateOrder(order);
                        }
                        else
                        {
                            int totalPrice;
                            totalPrice = dbc.GetTotalPrice(orderID);
                            TempData["summa"] = totalPrice;
                            TempData["Auth"] = "steg5";
                            TempData["campaigncodeText"] = "Du har angivit en ogiltig kampanjkod.";
                            //return RedirectToAction("Slutfor", "Boka");
                            return Redirect(Url.Action("Slutfor", "Boka") + "#slutfor-valda-prod");
                        }
                    }
                    dbc.UpdateOrder(order);

                    //Detta ska endast ske när mailet har skickats!!!!
                    // If(mail == true)
                    var productList = dbc.getOrderdetails(order.orderID);
                    emailer mailer = new emailer();
                    List<string> productName = new List<string>();

                    mailer.ToEmail = email;

                    mailer.Subject = "Bokningsförfrågan mottagen Leventsyd";
                    foreach (var item in productList)
                    {
                        productName.Add("<b>Produkt: </b>" +" "+ item.package.name.ToString() + " " + "<b>Antal: </b>" + item.amount.ToString() +" st");
                    }

                    mailer.Body = "<h1>Tack för din bokningsförfrågan " + "<b>" + cust.firstname + "</b>!</h1>" +
                    "<br/><br/>" +

                    "<h3>Ditt ordernummer är: <b>" + order.orderID.ToString() + "</b></h3>" +
                    "<br/><br/>" +
                    "<h2>Valda Produkter</h2>" + "<br/>" +



                    //här måste vi kunna skriva ut alla valda produkter också!!!!!! + antal
                    productName[0].ToString() + "<br/><br/>" +

                    "Order skapad: " + order.orderdate.ToString() + "<br/>" +
                    "Använd kampanjkod: " + kampanjkod.ToString() + "<br/>" +
                     "Totalsumma valda produkter: <font color=red>" + order.totalprice.ToString() + " Kr</font>" + 
                     "<br/><br/>"+



                    "<h2>Person- och leveransuppgifter: </h2> " + 
                    "<br/>" +
                    cust.firstname  + " " + cust.lastname + "<br/>" +
                    cust.email + "<br/>" +
                    cust.phonenumber + "<br/>" +
                    order.deliveryadress + "<br/>" +
                    order.zipcode + "<br/>" +
                    order.town + "<br/>" +
                    order.eventdate.ToString() 
                    
                    + "<br/><br/>" +
                    "Övriga frågor: " + order.requests +"<br/><br/><br/>" + 



                    "<img src="+"~/Content/bilder/logo1.jpg"+"/>";

                       


                        mailer.IsHtml = true;
                        mailer.Send();

                    //order.sent = "true";
                    //dbc.UpdateOrder(order);

                    //Måste även här skicka ett mail till alex med info!!!!

                    //måste sätta en auth.. så man inte kan komma åt i url sedan.....
                    TempData["klar"] = "true";

                        return RedirectToAction("Klar", "Boka", new { @id = orderID });
                
                   
                }

            }
            catch (SmtpFailedRecipientException ex)
            {
                SmtpStatusCode statusCode = ex.StatusCode;

                return RedirectToAction("Error", "Home");

            }


            TempData["Auth"] = "steg6";
            return RedirectToAction("Tillbehor", "Boka");
        }
        public ActionResult Klar(int id)
        {
            try
            {
                order ord = new order();
                ord = dbc.GetOrder(id);
                if(TempData["klar"] == "true")
                {
                    return View(ord);
                }
                else
                {
                    return RedirectToAction("index", "Boka");
                }
                

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}