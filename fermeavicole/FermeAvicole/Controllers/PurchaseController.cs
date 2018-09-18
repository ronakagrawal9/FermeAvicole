using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FermeAvicole.ViewModel;
namespace FermeAvicole.Controllers
{
    public class PurchaseController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PurchaseList()
        {
            PurchaseVM obj = new PurchaseVM();
            obj.PurchaseList = (from p in db.Purchases
                                select new PurchaseVM()
                                {
                                    Purchase_Id = p.Purchase_Id,
                                    Supplier_Name = p.Supplier.Supplier_Name,                                  
                                    Total_amount = p.Total_Amount,
                                    PurchaseItem=(from pi in db.PurchaseTransactions.Where(m=>m.Purchase_Id==p.Purchase_Id)
                                     select new PurchaseItemVM()
                                    {
                                         Item_Name=pi.PurchaseItem.Item_Name,
                                         Price=pi.Item_Price,  
                                         Quantity=pi.Quantity                                       

                                    }).FirstOrDefault()
                                 
                                }).ToList();
            return View(obj.PurchaseList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SupplierDDL = GetSupplierDDL();
            ViewBag.ItemDDL = GetItemListDDL();
            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseVM obj)
        {
            Purchase p = new Purchase();
            LoginVM objLogin = Helper.AppHelper.GetUserInfoDTOFromSession();
            p.Supplier_Id = obj.Supplier_Id;
            p.Date = obj.Purchase_Date;
            p.Total_Amount = obj.PurchaseItem.Quantity * obj.PurchaseItem.Price;
            db.Purchases.Add(p);
            db.SaveChanges();

            PurchaseTransaction pi = new PurchaseTransaction();
            pi.Purchase_Id = p.Purchase_Id;
            pi.PurchaseItem_Id = obj.PurchaseItem.PurchaseItem_Id;
            pi.Quantity = obj.PurchaseItem.Quantity;
            pi.Item_Price = obj.PurchaseItem.Price;
            pi.Total= obj.PurchaseItem.Quantity* obj.PurchaseItem.Price;
            db.PurchaseTransactions.Add(pi);
            db.SaveChanges();

            Stock oldstock = db.Stocks.Where(m => m.Item_Id == obj.PurchaseItem.PurchaseItem_Id).FirstOrDefault();
            if(oldstock!=null)
            {

                oldstock.Item_Id = obj.PurchaseItem.PurchaseItem_Id;
                oldstock.Current_Stock = oldstock.Current_Stock + obj.PurchaseItem.Quantity;
                oldstock.MinStock_Limit = 5;
              
                db.SaveChanges();
            }
            else
            {

                Stock s = new Stock();
                s.Item_Id = obj.PurchaseItem.PurchaseItem_Id;
                s.Current_Stock = obj.PurchaseItem.Quantity;
                s.MinStock_Limit = 5;
                db.Stocks.Add(s);
                db.SaveChanges();
            }


            return RedirectToAction("PurchaseList", "Purchase");
        }

        public  List<SelectListItem> GetSupplierDDL()
        {
            List<SelectListItem> lstSupplier = new List<SelectListItem>();
            var data = db.Suppliers.ToList();
            foreach(var item in data)
            {
                lstSupplier.Add(new SelectListItem()
                {

                    Text=item.Supplier_Name,
                    Value=item.Supplier_Id.ToString(),
                    Selected=item.Supplier_Id==0
                });
            }
            return lstSupplier;
        }

        public List<SelectListItem> GetItemListDDL() 
        {
            List<SelectListItem> LstItem = new List<SelectListItem>();
            var data = db.PurchaseItems.ToList();
            foreach (var item in data)
            {
                LstItem.Add(new SelectListItem()
                {

                    Text = item.Item_Name,
                    Value = item.PurchaseItem_Id.ToString(),
                    Selected = item.PurchaseItem_Id == 0
                });
            }
            return LstItem;
        }

     
      

    }
}