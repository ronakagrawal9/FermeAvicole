using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FermeAvicole.Controllers
{
    public class StockController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Stock
        public ActionResult CurrentStock()
        {
            {
                List<StockVM> CurrentStock = new List<StockVM>();
                CurrentStock = (from cs in db.Stocks
                             select new StockVM()
                             {
                                 Item_Id=cs.Item_Id,
                                 Item_Name=cs.Item.Item_Name,
                                 Current_Stock=cs.Current_Stock


                             }).ToList();
                return View(CurrentStock);
            }
        }
    }
}