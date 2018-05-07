using LarnerhemsEvent.DBOperations;
using LarnerhemsEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LarnerhemsEvent.Controllers
{
    public class BokaController : Controller
    {
        private DbOp dbc = new DbOp();
        List<package> tentpack = new List<package>();
        List<package> floorPack = new List<package>();

        // GET: Boka
        public ActionResult Index()
        {
            tentpack = dbc.GetTentPackages();
            floorPack = dbc.GetFloorPackages();
            

            return View(tentpack);
        }
    }
}