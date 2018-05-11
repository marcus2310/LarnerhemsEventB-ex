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
        int[] ArrayofPackageID;




        VMPackages Packages = new VMPackages();

        // GET: Boka
        public ActionResult Index()
        {
            tentpackList = dbc.GetTentPackages();
          
            

            return View(tentpackList);
        }
        [HttpPost]
        public ActionResult Index(package pg)
        {
            tentpackList = dbc.GetTentPackages();



            return View(tentpackList);
        }
        public ActionResult Golv()
        {
            floorPackList = dbc.GetFloorPackages();

            return View (floorPackList);
        }

        public ActionResult Ljud()
        {
            soundPackList = dbc.GetSoundPackages();

            return View(soundPackList);
        }

        public ActionResult Ljus()
        {
            LightPackList = dbc.GetLightPackages();

            return View(LightPackList);
        }

        public ActionResult Tillbehor()
        {
            ExtraPackList = dbc.GetExtraPackages();

            return View(ExtraPackList);
        }
        public ActionResult Slutfor()
        {
            allPackList = dbc.GetAllPackages();

            return View(allPackList);
        }
    }
}