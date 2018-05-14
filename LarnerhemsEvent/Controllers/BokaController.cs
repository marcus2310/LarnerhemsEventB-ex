using LarnerhemsEvent.DBOperations;
using LarnerhemsEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LarnerhemsEvent.Models.ViewModels; 

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

        List<string> PackageIDList = new List<string>();


        List<package> ChoosenPackages = new List<package>();




        VMPackages Packages = new VMPackages();

        // GET: Boka
        public ActionResult Index()
        {
            tentpackList = dbc.GetTentPackages();
          
            

            return View(tentpackList);
        }
       
       
        [HttpPost]
      
        public ActionResult Index(FormCollection form)
        {
            try
            {
                var PackageId = form["buttonItem"];
                int itemID = Convert.ToInt32(PackageId);

                var item = dbc.GetAPackage(itemID);
                TempData["tentItem"] = item.packageID;

            }
            catch (Exception)
            {



            }
            

            return RedirectToAction("Golv", "Boka");
        }

            public ActionResult Golv()
        {
            floorPackList = dbc.GetFloorPackages();

            return View (floorPackList);
        }
        [HttpPost]
        public ActionResult Golv(FormCollection form)
        {
            try
            {
                var PackageId = form["buttonItem"];
                int itemID = Convert.ToInt32(PackageId);

                var item = dbc.GetAPackage(itemID);

                TempData["floorItem"] = item.packageID;

            }
            catch (Exception)
            {

            }

            return RedirectToAction("Ljud", "Boka");
        }

        public ActionResult Ljud()
        {
            soundPackList = dbc.GetSoundPackages();

            return View(soundPackList);
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

            return RedirectToAction("Ljus", "Boka");
        }

        public ActionResult Ljus()
        {
            LightPackList = dbc.GetLightPackages();

            return View(LightPackList);
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

            return RedirectToAction("Tillbehor", "Boka");
        }
        public ActionResult Tillbehor()
        {
            ExtraPackList = dbc.GetExtraPackages();

            return View(ExtraPackList);
        }
        [HttpPost]
        public ActionResult Tillbehor(FormCollection form)
        {
            try
            {
                var PackageId1 = form["13"];
                var PackageId2 = form["14"];
                var PackageId3 = form["15"];
                var PackageId4 = form["16"];
                var PackageId5 = form["17"];
                var PackageId6 = form["18"];

                int itemID1 = Convert.ToInt32(PackageId1);
                int itemID2 = Convert.ToInt32(PackageId2);
                int itemID3 = Convert.ToInt32(PackageId3);
                int itemID4 = Convert.ToInt32(PackageId4);
                int itemID5 = Convert.ToInt32(PackageId5);
                int itemID6 = Convert.ToInt32(PackageId6);

                var item1 = dbc.GetAPackage(13);
                var item2 = dbc.GetAPackage(14);
                var item3 = dbc.GetAPackage(15);
                var item4 = dbc.GetAPackage(16);
                var item5 = dbc.GetAPackage(17);
                var item6 = dbc.GetAPackage(18);



                TempData["TillbehorItem1"] = item1.packageID;
                TempData["TillbehorItem2"] = item2.packageID;
                TempData["TillbehorItem3"] = item3.packageID;
                TempData["TillbehorItem4"] = item4.packageID;
                TempData["TillbehorItem5"] = item5.packageID;
                TempData["TillbehorItem6"] = item6.packageID;
            }
            catch (Exception)
            {

               
            }

            return RedirectToAction("Slutfor", "Boka");
        }
        public ActionResult Slutfor()
        {
            foreach (var i in TempData)
            {
                var tempItem = Convert.ToString(i.Value);
                PackageIDList.Add(tempItem);

            }

            allPackList = dbc.GetAllPackages();

            

            return View(allPackList);
        }
    }
}