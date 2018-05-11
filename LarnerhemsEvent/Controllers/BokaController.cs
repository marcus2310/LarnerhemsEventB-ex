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
        VMPackages Packages = new VMPackages();

        // GET: Boka
        public ActionResult Index()
        {
            tentpackList = dbc.GetTentPackages();
            floorPackList = dbc.GetFloorPackages();
            allPackList = dbc.GetAllPackages(); 

            return View(allPackList);
        }
       

    }
}