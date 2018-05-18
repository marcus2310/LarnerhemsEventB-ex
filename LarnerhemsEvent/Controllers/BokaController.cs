﻿using LarnerhemsEvent.DBOperations;
using LarnerhemsEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LarnerhemsEvent.Models.ViewModels;
using System.Web.Security;

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
                TempData["tentItem"] = item.packageID;
                dbc.AddToPackOrderDetail(orderID, itemID, 1);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Boka");
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
                return RedirectToAction("Index", "Boka");

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
                TempData["floorItem"] = item.packageID;
                dbc.AddToPackOrderDetail(orderID, itemID, 1);


            }
            catch (Exception)
            {

            }
            var tillbaka = form["tillbaka"];

            if (tillbaka != null)
            {
                //här kan vi sedan ta bort paket som valts

                return RedirectToAction("Index", "Boka");
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
                return RedirectToAction("Index", "Boka");

            }
            
        }
        [HttpPost]
        public ActionResult Ljud(FormCollection form)
        {
            try
            {
                var PackageId = form["buttonItem"];
                int itemID = Convert.ToInt32(PackageId);

                var item = dbc.GetAPackage(itemID);

                TempData["soundItem"] = item.packageID;

            }
            catch (Exception)
            {

            }
            var tillbaka = form["tillbaka"];

            if (tillbaka != null)
            {


                TempData["auth"] = "steg2";
                return RedirectToAction("Golv", "Boka");
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
                return RedirectToAction("Index", "Boka");

            }
           
        }
        [HttpPost]
        public ActionResult Ljus(FormCollection form)
        {
            try
            {
                var PackageId = form["buttonItem"];
                int itemID = Convert.ToInt32(PackageId);

                var item = dbc.GetAPackage(itemID);

                TempData["lightItem"] = item.packageID;

            }
            catch (Exception)
            {

            }
            var tillbaka = form["tillbaka"];

            if (tillbaka != null)
            {
                TempData["auth"] = "steg3";
                return RedirectToAction("Ljud", "Boka");
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
                return RedirectToAction("Index", "Boka");
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

               

                if(PackageValue1 != "" && PackageValue1 != "0" && PackageValue1 != "00")
                {
                    int itemAmount1 = Convert.ToInt32(PackageValue1);
                    TempData["TillbehorItem1"] = item1.packageID;
                    TempData["TillbehorAmount1"] = itemAmount1;
                }
                if (PackageValue2 != "" && PackageValue2 != "0" && PackageValue2 != "00")
                {
                    int itemAmount2 = Convert.ToInt32(PackageValue2);
                    TempData["TillbehorItem2"] = item2.packageID;
                    TempData["TillbehorAmount2"] = itemAmount2;
                }
                if (PackageValue3 != "" && PackageValue3 != "0" && PackageValue3 != "00")
                {

                    int itemAmount3 = Convert.ToInt32(PackageValue3);
                    TempData["TillbehorItem3"] = item3.packageID;
                    TempData["TillbehorAmount3"] = itemAmount3;

                }
                if (PackageValue4 != "" && PackageValue4!= "0" && PackageValue4 != "00")
                {
                    int itemAmount4 = Convert.ToInt32(PackageValue4);
                    TempData["TillbehorItem4"] = item4.packageID;
                    TempData["TillbehorAmount4"] = itemAmount4;

                }
                if (PackageValue5 != "" && PackageValue5 != "0" && PackageValue5 != "00")
                {
                    int itemAmount5 = Convert.ToInt32(PackageValue5);
                    TempData["TillbehorItem5"] = item5.packageID;
                    TempData["TillbehorAmount5"] = itemAmount5;

                }
                if (PackageValue6 != "" && PackageValue6 != "0" && PackageValue6 != "00")
                {
                    int itemAmount6 = Convert.ToInt32(PackageValue6);
                    TempData["TillbehorItem6"] = item6.packageID;
                    TempData["TillbehorAmount6"] = itemAmount6;

                }




            }
            catch (Exception)
            {

               
            }
            var tillbaka = form["tillbaka"];

            if (tillbaka != null)
            {
                TempData["auth"] = "steg4";
                return RedirectToAction("Ljus", "Boka");
            }
            TempData["Auth"] = "steg5";
            return RedirectToAction("Slutfor", "Boka");
        }


        public ActionResult Slutfor()
        {
            List<package> selectedPackList = new List<package>();
            List<int> PackageIDList = new List<int>();
            try
            {
                if (TempData["Auth"].ToString() == "steg5" || TempData["Auth"].ToString() == "steg6")
                {
                    foreach (var i in TempData)
                    {
                        var tempItem = i.Value;

                        if(tempItem !="steg5" && i.Key != "TillbehorAmount1" && i.Key != "TillbehorAmount2" && i.Key != "TillbehorAmount3" && i.Key != "TillbehorAmount4" && i.Key != "TillbehorAmount5" && i.Key != "TillbehorAmount6")
                        {
                         
                            
                                PackageIDList.Add(Convert.ToInt32(tempItem));
                            
                                

                        }
                        else
                        {

                        }   
                        
                       

                    }

                    selectedPackList = dbc.GetSelectedPackages(PackageIDList);

            

                    return View(selectedPackList);
                }
                else
                {
                    return RedirectToAction("Index", "Boka");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Boka");
            }

        }
        [HttpPost]
        public ActionResult Slutfor(FormCollection form)
        {
            try
            {


            }
            catch (Exception)
            {

            }
            var tillbaka = form["tillbaka"];

            if (tillbaka != null)
            {
                TempData["auth"] = "steg5";
                return RedirectToAction("Tillbehor", "Boka");
            }

            TempData["Auth"] = "steg6";
            return RedirectToAction("Tillbehor", "Boka");
        }
    }
}