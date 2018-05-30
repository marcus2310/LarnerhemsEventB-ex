using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LarnerhemsEvent.Models;
using System.Data.Entity.Validation;

namespace LarnerhemsEvent.DBOperations
{
    public class DbOp
    {
        private leventsyd_se_dbEntities3 db = new leventsyd_se_dbEntities3();



        //hämtar ett specifikt paket med ID

        #region Lägg till i DB
        public int CreateOrder()
        {
            order ord = new order();
            ord.orderdate = DateTime.Today;
            ord.approved = "false";
            ord.sent = "false";
            ord.totalprice = 0;
            ord.orderID = ord.orderID;

            try
            {
                db.orders.Add(ord);
                db.SaveChanges();
                return ord.orderID;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            return 0;

        }
        public int CreateCustomer(customer cust)
        {
            try
            {
                db.customers.Add(cust);
                db.SaveChanges();
                return cust.customerID;
            }
            catch(Exception)
            {
                
            }
            return 0;
        }
        public void AddToPackOrderDetail(int OrderID, int PacketID, int amount)
        {
            packageorderdetail PackOrder = new packageorderdetail();

            try
            {
                PackOrder.fk_order_id = OrderID;
                PackOrder.fk_package_id = PacketID;
                PackOrder.amount = amount;


                db.packageorderdetails.Add(PackOrder);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }

        }
        public void SetTotalpriceOrder(int orderID, int totalprice)
        {
            try
            {
                var order = db.orders.Find(orderID);

                order.totalprice = totalprice;
                db.SaveChanges();

            }
            catch (Exception)
            {


            }


        }

        public void CreateCampaignCode(string code, int amount)
        {
            campaigncode camp = new campaigncode();
            camp.code = code;
            camp.amount = amount;
            camp.campaigncodeID = camp.campaigncodeID;
            db.campaigncodes.Add(camp);
            db.SaveChanges();

        }
 

        #endregion

        #region Hämta från databas
        public int GetTotalPrice(int OrderID)
        {
            int TotalPrice = 0;
            int pricePerItem;

            var orders = db.packageorderdetails.Where(x => x.fk_order_id == OrderID).ToList();

            foreach (var item in orders)
            {
                pricePerItem = Convert.ToInt32(item.package.price) * Convert.ToInt32(item.amount);
                TotalPrice = TotalPrice + pricePerItem;
            }

            return TotalPrice;
        }
        public package GetAPackage(int id)
        {

            var pack = db.packages.Find(id);


            return pack;
        }
        //hämta en specifik order och skicka tillbaka paket som ligger i den ordern.
        //public List<package> GetOrderPackages(int id)
        //{

        //    //var order = db.orders.Find(id);

        //    //var OrderPackages = order.packageorderdetails.ToList();

        //    return OrderPackages;
        //}
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
        public List<package> GetAllPackages()
        {
            var packages = db.packages.ToList();

            return packages;
        }
        public List<package> GetSelectedPackages(List<int> id)
        {
            var allPacks = db.packages.ToList();
            List<package> SelectedPacks = new List<package>();
            foreach (var item in allPacks)
            {
                foreach (var item2 in id)
                {
                    if (item.packageID == item2)
                    {
                        SelectedPacks.Add(item);

                    }
                }

            }

            return SelectedPacks;

        }
        public product GetProduct(int id)
        {
            var theProduct = db.products.Find(id);

            return theProduct;
        }
        public List<packageorderdetail> getOrderdetails(int orderID)
        {
            List<packageorderdetail> SelectedPackOrdList = new List<packageorderdetail>();

            SelectedPackOrdList = db.packageorderdetails.Where(x => x.fk_order_id == orderID).ToList();

            return SelectedPackOrdList;

        }
        public order GetOrder(int orderID)
        {
            order TheOrder = new order();
            TheOrder = db.orders.Find(orderID);

            return TheOrder;

        }

        public bool Login(string username, string password)
        {
            try
            {
                var user = db.users.Where(x => x.username == username && x.password == password).ToList();

                if(user.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }

            
            
        }
        public campaigncode GetCampaigncode(string code)
        {
            campaigncode camp = new campaigncode();
            var campaign = db.campaigncodes.Where(x => x.code == code).ToList();

            if (campaign.Count == 1)
            {
                foreach (var item in campaign)
                {
                    camp = item;

                }
                return camp;
            }
            else
            {
                return null;
            }
           
        }
        #endregion

        #region Tabort Från DB
 public void DeleteUnfinnishedOrder()
        {
            var time = DateTime.Today;
            var selectedOrderdetailsList = db.packageorderdetails.Where(x => x.order.sent == "false" && x.order.orderdate < time).ToList();
            var selectedOrderList = db.orders.Where(x => x.sent == "false" && x.orderdate < time).ToList();


            try
            {
                foreach (var i in selectedOrderdetailsList)
                {
                    db.Entry(i).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                foreach (var item in selectedOrderList)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {

               
            }
            
        }
        //tabort ett visst packet i orden.
        public void DeleteSelectedPackage(int OrderID, int GenreID)
        {
            packageorderdetail packOrd = new packageorderdetail();

            var PackOrdList = db.packageorderdetails.Where(x => x.fk_order_id == OrderID && x.package.fk_genre_id == GenreID).ToList();

            try
            {
                foreach (var item in PackOrdList)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                
            }
           

        }
        public void DeleteSelectedPackageByID(int OrderID, int packageID)
        {
            packageorderdetail packOrd = new packageorderdetail();

            var PackOrdList = db.packageorderdetails.Where(x => x.fk_order_id == OrderID && x.package.packageID == packageID).ToList();

            try
            {
                foreach (var item in PackOrdList)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {

            }


        }


        #endregion

        #region Uppdatera DB

        public void UpdateOrder(order order)
        {
            try
            {
                order ord = db.orders.Single(x => x.orderID == order.orderID);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void SentMailTrue(order order)
        {
            try
            {
                order ord = db.orders.Single(x => x.orderID == order.orderID);
                ord.sent = "true";
                db.SaveChanges();

            }
            catch (Exception)
            {

            }


        }
        public void ApprovedOrder(order order)
        {
            order ord = db.orders.Single(x => x.orderID == order.orderID);
            ord.approved = "true";
            db.SaveChanges();

        }
        public void UpdateCampaignCode(string name, int amount)
        {
            try
            {
                var campaignCode = db.campaigncodes.Find(1);

                campaignCode.code = name;
                campaignCode.amount = amount;

                db.SaveChanges();

            }
            catch (Exception)
            {

            }

        }
        #endregion


    }
}