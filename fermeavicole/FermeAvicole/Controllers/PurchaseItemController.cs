using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FermeAvicole.ViewModel;
using System.IO;
namespace FermeAvicole.Controllers
{
    public class PurchaseItemController : baseuserController
    {
        // GET: PurchaseItem
        FermeDBEntities db = new FermeDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PurchaseItemList()
        {
            List<PurchaseItemVM> lstItems = new List<PurchaseItemVM>();
            lstItems = (from i in db.PurchaseItems
                        select new PurchaseItemVM()
                        {
                            PurchaseItem_Id = i.PurchaseItem_Id,
                            Item_Name = i.Item_Name,
                            Item_Price = i.Price,
                            Image_Path = i.Image_Path,
                            Item_Description = i.Item_Description,
                            Category_Name = i.PurchaseCategory.Category_Name

                        }).ToList();
            return View(lstItems);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DDLPurchaseCategory = DDLPurchaseCategory();
            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseItemVM obj)
        {
            string filExtension = "";
            if (ModelState.IsValid)
            {
                if (obj.Item_Image.FileName != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.Item_Image.FileName);
                    string path = Server.MapPath("~/Upload/PurchaseItem/" + fileGuid.ToString() + filExtension);
                    obj.Item_Image.SaveAs(path);
                    obj.Image_Path = "/Upload/PurchaseItem/" + fileGuid.ToString() + filExtension;// path to store file in database
                }

                PurchaseItem i = new PurchaseItem();
                i.Item_Name = obj.Item_Name;
                i.Item_Description = obj.Item_Name;
                i.Price = obj.Item_Price;
                i.Image_Path = obj.Image_Path;
                i.PurchaseCategory_Id = obj.PurchaseCategory_Id;
                db.PurchaseItems.Add(i);
                db.SaveChanges();
                return RedirectToAction("PurchaseItemList", "PurchaseItem");

            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PurchaseItemVM iVM = new PurchaseItemVM();
            iVM = (from i in db.PurchaseItems.Where(m => m.PurchaseItem_Id == id)
                   select new PurchaseItemVM()
                   {
                       PurchaseItem_Id = i.PurchaseItem_Id,
                       Item_Name = i.Item_Name,
                       Item_Price = i.Price,
                       Image_Path = i.Image_Path,
                       Item_Description = i.Item_Description,
                       PurchaseCategory_Id = i.PurchaseCategory_Id

                   }).FirstOrDefault();
            ViewBag.DDLPurchaseCategory = DDLPurchaseCategory();
            return View(iVM);
        }

        [HttpPost]
        public ActionResult Edit(PurchaseItemVM obj)
        {
            string filExtension = "";
            if (ModelState.IsValid)
            {
                if (obj.Item_Image != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.Item_Image.FileName);
                    string path = Server.MapPath("~/Upload/PurchaseItem/" + fileGuid.ToString() + filExtension);
                    obj.Item_Image.SaveAs(path);
                    obj.Image_Path = "/Upload/PurchaseItem/" + fileGuid.ToString() + filExtension;// path to store file in database
                }

                PurchaseItem i;
                i = db.PurchaseItems.Where(m => m.PurchaseItem_Id == obj.PurchaseItem_Id).FirstOrDefault();
                i.Item_Name = obj.Item_Name;
                i.Item_Description = obj.Item_Name;
                i.Price = obj.Item_Price;
                i.Image_Path = obj.Image_Path;
                i.PurchaseCategory_Id = obj.PurchaseCategory_Id;

                db.SaveChanges();
                return RedirectToAction("PurchaseItemList", "PurchaseItem");

            }
            else
            {
                ViewBag.DDLPurchaseCategory = DDLPurchaseCategory();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Item i;
            i = db.Items.Where(m => m.Item_Id == id).FirstOrDefault();
            if (i != null)
            {
                db.Items.Remove(i);
                db.SaveChanges();
                return RedirectToAction("PurchaseItemList", "PurchaseItem");
            }
            return View();
        }

        public List<SelectListItem> DDLPurchaseCategory()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();
            var data = db.PurchaseCategories.ToList();
            foreach (var item in data)
            {
                lstCategory.Add(new SelectListItem()
                {
                    Text = item.Category_Name,
                    Value = item.PurchaseCategory_Id.ToString(),
                    Selected = item.PurchaseCategory_Id == 0

                });
            }
            return lstCategory;
        }
    }
}