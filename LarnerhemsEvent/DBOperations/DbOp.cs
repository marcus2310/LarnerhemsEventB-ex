using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LarnerhemsEvent.Models;

namespace LarnerhemsEvent.DBOperations
{
    public class DbOp
    {
        private leventsyd_se_dbEntities1 db = new leventsyd_se_dbEntities1();



        public List<package> GetSmallTentPackage()
        {
            var Smalltentpackage = db.packages.Where(x => x.packageID == 1).ToList();

            return Smalltentpackage;
        }

    }
}