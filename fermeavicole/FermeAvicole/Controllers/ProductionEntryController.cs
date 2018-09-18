using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FermeAvicole.ViewModel;
namespace FermeAvicole.Controllers
{

    public class ProductionEntryController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: ProductionEntry
        public ActionResult Index()
        {
            return View();
        }


        public List<SelectListItem> GetItemDDL()
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            var data = db.Items.ToList();
            foreach (var item in data)
            {
                lstItem.Add(new SelectListItem()
                {
                    Text = item.Item_Name,
                    Value = item.Item_Id.ToString(),
                    Selected = item.Item_Id == 0

                });
            }
            return lstItem;
        }
        public ActionResult ProductionEntryList()
        {
            ProductionEntryVM obj = new ProductionEntryVM();
            obj.ProductionEntryList = (from p in db.ProductionEntries
                                       select new ProductionEntryVM()
                                       {
                                           ProductionEntry_Id = p.ProductionEntry_Id,
                                           Quantity = p.Quantity,
                                           Date = p.Date,
                                           Item_Name=p.Item.Item_Name
                                       }).ToList();
            return View(obj.ProductionEntryList);
        }
        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.ItemDDL = GetItemDDL();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductionEntryVM obj)
        {
            ProductionEntry p = new ProductionEntry();
            LoginVM objLogin = Helper.AppHelper.GetUserInfoDTOFromSession();
            p.Item_Id = obj.Item_Id;
            p.Date = obj.Date;
            p.Quantity = obj.Quantity;
            p.User_Id = objLogin.UserID;
            db.ProductionEntries.Add(p);
            db.SaveChanges();

            Stock oldstock = db.Stocks.Where(m => m.Item_Id == p.Item_Id).FirstOrDefault();
            if (oldstock != null)
            {

                oldstock.Item_Id = p.Item_Id;
                oldstock.Current_Stock = oldstock.Current_Stock + obj.Quantity;
                oldstock.MinStock_Limit = 5;
                db.SaveChanges();

            }
            else
            {

                Stock s = new Stock();
                s.Item_Id = p.Item_Id;
                s.Current_Stock = obj.Quantity;
                s.MinStock_Limit = 5;
                db.Stocks.Add(s);
                db.SaveChanges();
            }

            return RedirectToAction("ProductionEntryList", "ProductionEntry");
        }
    }
}