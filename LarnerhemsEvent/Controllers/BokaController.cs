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
        

        // GET: Boka
        public ActionResult Index()
        {
            List<package> smalltent = new List<package>();
            
            smalltent = dbc.GetSmallTentPackage();

            return View();
        }
    }
}