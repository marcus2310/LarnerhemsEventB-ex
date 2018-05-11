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
        private leventsyd_se_dbEntities2 db = new leventsyd_se_dbEntities2();



      
        public List<package> GetTentPackages()
        {
            var tentpackages = db.packages.Where(x => x.fk_genre_id == 1).ToList();

            return tentpackages;
        }
        public List<package> GetFloorPackages()
        {
            var floorPackages = db.packages.Where(x => x.fk_genre_id == 2).ToList();

            return floorPackages;
        }

        public List<package> GetSoundPackages()
        {
            var SoundPackages = db.packages.Where(x => x.fk_genre_id == 3).ToList();

            return SoundPackages;
        }
        public List<package> GetLightPackages()
        {
            var LightPackages = db.packages.Where(x => x.fk_genre_id == 4).ToList();

            return LightPackages;
        }

        public List<package> GetExtraPackages()
        {
            var ExtraPackages = db.packages.Where(x => x.fk_genre_id == 5).ToList();

            return ExtraPackages;
        }

        public List<package>GetAllPackages()
        {
            var packages = db.packages.ToList(); 

            return packages; 
        }



        //public List<prodpackdetails> GetAllPackProd()
        //{
        //    var packprod = db.packages.Where( )
        //}
    }
}