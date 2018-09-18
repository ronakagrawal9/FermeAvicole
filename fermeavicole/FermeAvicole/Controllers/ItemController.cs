using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FermeAvicole.ViewModel;
using System.IO;

namespace FermeAvicole.Controllers
{
    public class ItemController : baseuserController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Item
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult ItemList()
        {
            List<ItemVM> lstItems = new List<ItemVM>();
            lstItems = (from i in db.Items
                        select new ItemVM()
                        {
                            Item_Id = i.Item_Id,
                            Item_Name = i.Item_Name,
                            Item_Price = i.Price,
                            Image_Path = i.Image_Path,
                            Item_Description = i.Item_Description,
                            Category_Name = i.Category.Category_Name

                        }).ToList();
            return View(lstItems);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DDLCategory = DDLCategory();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemVM obj)
        {
            string filExtension = "";
            if (ModelState.IsValid)
            {
                if (obj.Item_Image.FileName != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.Item_Image.FileName);
                    string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                    obj.Item_Image.SaveAs(path);
                    obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                }
           
                Item i = new Item();
                i.Item_Name = obj.Item_Name;
                i.Item_Description = obj.Item_Name;
                i.Price = obj.Item_Price;
                i.Image_Path = obj.Image_Path;
                i.Category_Id = obj.Category_Id;
                db.Items.Add(i);
                db.SaveChanges();
                return RedirectToAction("ItemList", "Item");

            }
            else
            {
                return View();
            }
          
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ItemVM iVM = new ItemVM();
            iVM = (from i in db.Items.Where(m => m.Item_Id == id)
                 select new ItemVM()
                 {
                     Item_Id=i.Item_Id,
                     Item_Name=i.Item_Name,
                     Item_Price=i.Price,
                     Image_Path=i.Image_Path,
                     Item_Description=i.Item_Description,
                     Category_Id=i.Category_Id

                 }).FirstOrDefault();
            ViewBag.DDLCategory = DDLCategory();
            return View(iVM);
        }

        [HttpPost]
        public ActionResult Edit(ItemVM obj)
        {
            string filExtension = "";
            if (ModelState.IsValid)
            {
                if (obj.Item_Image != null)
                {
                    Guid fileGuid = Guid.NewGuid();
                    filExtension = Path.GetExtension(obj.Item_Image.FileName);
                    string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                    obj.Item_Image.SaveAs(path);
                    obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                }

                Item i;
                i = db.Items.Where(m => m.Item_Id == obj.Item_Id).FirstOrDefault();
                i.Item_Name = obj.Item_Name;
                i.Item_Description = obj.Item_Description;
                i.Price = obj.Item_Price;
                i.Image_Path = obj.Image_Path;
                i.Category_Id = obj.Category_Id;
              
                db.SaveChanges();
                return RedirectToAction("ItemList", "Item");

            }
            else
            {
                ViewBag.DDLCategory = DDLCategory();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Item i;
            i = db.Items.Where(m => m.Item_Id == id).FirstOrDefault();
            if(i!=null)
            {
                db.Items.Remove(i);
                db.SaveChanges();
                return RedirectToAction("ItemList", "Item");
            }
            return View();
        }

        public List<SelectListItem> DDLCategory()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();
            var data = db.Categories.ToList();
            foreach(var item in data)
            {
                lstCategory.Add(new SelectListItem()
                {
                    Text = item.Category_Name,
                    Value = item.Category_Id.ToString(),
                    Selected = item.Category_Id == 0

                });
            }
            return lstCategory;
        }

    }
}