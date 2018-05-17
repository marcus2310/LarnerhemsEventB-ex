using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LarnerhemsEvent.Models;
using LarnerhemsEvent.DBOperations;

namespace LarnerhemsEvent.Controllers
{
    public class ProduktInfoController : Controller
    {
                private DbOp dbc = new DbOp();
        // GET: ProduktInfo
        public ActionResult Index(int id)
        {
            product prod = new product();
            prod = dbc.GetProduct(id);

            return View(prod);
        }
    }
}