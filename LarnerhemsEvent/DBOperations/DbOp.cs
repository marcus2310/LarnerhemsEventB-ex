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



        //hämtar ett specifikt paket med ID
        public package GetAPackage(int id)
        {

            var pack = db.packages.Find(id);


            return pack;
        }
        //hämta en specifik order och skicka tillbaka paket som ligger i den ordern.
        public List<package> GetOrderPackages(int id)
        {

            var order = db.orders.Find(id);

            var OrderPackages = order.packages.ToList();

            return OrderPackages;
        }
        //hämtar alla tältpaket
        public List<package> GetTentPackages()
        {
            var tentpackages = db.packages.Where(x => x.fk_genre_id == 1).ToList();

            return tentpackages;
        }
        //hämtar alla golvpaket
        public List<package> GetFloorPackages()
        {
            var floorPackages = db.packages.Where(x => x.fk_genre_id == 2).ToList();

            return floorPackages;
        }
        //hämtar alla ljudpaket
        public List<package> GetSoundPackages()
        {
            var SoundPackages = db.packages.Where(x => x.fk_genre_id == 3).ToList();

            return SoundPackages;
        }
        //hämtar alla ljuspaket
        public List<package> GetLightPackages()
        {
            var LightPackages = db.packages.Where(x => x.fk_genre_id == 4).ToList();

            return LightPackages;
        }
        //hämtar alla tillbehör
        public List<package> GetExtraPackages()
        {
            var ExtraPackages = db.packages.Where(x => x.fk_genre_id == 5).ToList();

            return ExtraPackages;
        }
        //hämtar alla paket som finns
        public List<package>GetAllPackages()
        {
            var packages = db.packages.ToList(); 

            return packages; 
        }

    }
}