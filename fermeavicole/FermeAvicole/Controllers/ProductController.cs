using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FermeAvicole.ViewModel;
using System.IO;
using System.Web.Security;
using FermeAvicole.Helper;
using System.Net.Mail;
using System.Net;

namespace FermeAvicole.Controllers
{
    public class ProductController : baseCustomerController
    {
        // GET: Product

        FermeDBEntities db = new FermeDBEntities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Checkout()
        {
            ItemVM obj = new ItemVM();
            obj.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/" + "_AfterLoginLayout.cshtml";
            obj.ItemList = Helper.AppHelper.GetItemInfoFromSession();
            obj.Total_Amount = obj.ItemList.Sum(m => m.Amount);
            return View(obj);
        }

        [HttpGet]

        public ActionResult MakePaymentCash()
        {
            ItemVM obj = new ItemVM();
            obj.ItemList = Helper.AppHelper.GetItemInfoFromSession();
            obj.Total_Amount = obj.ItemList.Sum(m => m.Amount);
            return View(obj);
        }

        [HttpGet]

        public ActionResult OrderSuccess()
        {
            ViewBag.Message = "We have Successfully generated order";
            return View();
        }

        [HttpPost]

        public ActionResult MakePaymentCash(ItemVM obj)
        {

            Order o = new Order();
            o.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
            o.Date = System.DateTime.Now;
            o.Total_Amount = obj.Total_Amount;
            o.OrderStatus_Id = 1;
            db.Orders.Add(o);
            db.SaveChanges();


            obj.ItemList = Helper.AppHelper.GetItemInfoFromSession();
            foreach (var item in obj.ItemList)
            {
                OrderTransaction ot = new OrderTransaction();
                ot.Order_Id = o.Order_Id;
                ot.Item_Id = item.Item_Id;
                ot.Quantity = item.Quantity;
                ot.Amount = item.Amount;
                ot.Total = item.Amount * item.Quantity;
                db.OrderTransactions.Add(ot);
                db.SaveChanges();

                Stock oldstock = db.Stocks.Where(m => m.Item_Id == item.Item_Id).FirstOrDefault();
                if (oldstock != null)
                {
                    oldstock.Current_Stock = oldstock.Current_Stock - item.Quantity;
                    oldstock.MinStock_Limit = 5;
                    db.SaveChanges();

                }
            }

            PaymentTransaction pt = new PaymentTransaction();
            pt.Date = System.DateTime.Now;
            pt.Order_Id = o.Order_Id;
            pt.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
            pt.Amount = obj.Total_Amount;
            pt.Payment_Mode = "Cash";
            List<ItemVM> lstItem = null;
            Helper.AppHelper.SaveItemInfoInSession(lstItem);
            return RedirectToAction("OrderSuccess", "Product");
        }

        [HttpGet]

        public ActionResult MakePaymentOnline()
        {
            ItemVM obj = new ItemVM();
            obj.ItemList = Helper.AppHelper.GetItemInfoFromSession();
            obj.Total_Amount = obj.ItemList.Sum(m => m.Amount);
            return View(obj);
        }

        [HttpPost]

        public ActionResult MakePaymentOnline(ItemVM obj)
        {

            Order o = new Order();
            o.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
            o.Date = System.DateTime.Now;
            o.Total_Amount = obj.Total_Amount;
            o.OrderStatus_Id = 1;
            db.Orders.Add(o);
            db.SaveChanges();
           
            obj.ItemList = Helper.AppHelper.GetItemInfoFromSession();
            foreach (var item in obj.ItemList)
            {
                OrderTransaction ot = new OrderTransaction();
                ot.Order_Id = o.Order_Id;
                ot.Item_Id = item.Item_Id;
                ot.Quantity = item.Quantity;
                ot.Amount = item.Amount;
                ot.Total = item.Amount * item.Quantity;
                db.OrderTransactions.Add(ot);
                db.SaveChanges();

                Stock oldstock = db.Stocks.Where(m => m.Item_Id == item.Item_Id).FirstOrDefault();
                if (oldstock != null)
                {
                    oldstock.Current_Stock = oldstock.Current_Stock - item.Quantity;
                    oldstock.MinStock_Limit = 5;
                    db.SaveChanges();

                }
            }

            PaymentTransaction pt = new PaymentTransaction();
            pt.Date = System.DateTime.Now;
            pt.Order_Id = o.Order_Id;
            pt.User_Id = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
            pt.Amount = obj.Total_Amount;
            pt.Payment_Mode = "Online";
            db.PaymentTransactions.Add(pt);
            db.SaveChanges();
            List<ItemVM> lstItem = null;
            Helper.AppHelper.SaveItemInfoInSession(lstItem);
            string from = "ronak94279@gmail.com";
            string to = Helper.AppHelper.GetUserInfoDTOFromSession().Email_Id;
            string pwd = "ronak99999";
            string emailBody = string.Empty;
            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/Templetes/OrderConfirmation.html")))
            {
                emailBody = sr.ReadToEnd();
            }

            emailBody = emailBody.Replace("$OrderNumber$", o.Order_Id.ToString());
            emailBody = emailBody.Replace("$RecipientMessage$", "We have received your Order request. We will contact you soon");

            using (MailMessage mail = new MailMessage(from, to))
            {
                mail.Subject = "Order Booking";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from, pwd);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
            }
            return RedirectToAction("OrderSuccess","Product");
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Checkout(ItemVM obj)
        {
            if (obj.Payment_Mode == "Cash")
            {
                return RedirectToAction("MakePaymentCash", "Product");
            }

            if (obj.Payment_Mode == "Online")
            {
                return RedirectToAction("MakePaymentOnline", "Product");
            }
           
            obj.ItemList = Helper.AppHelper.GetItemInfoFromSession();
            obj.Total_Amount = obj.ItemList.Sum(m => m.Amount);
            return View(obj);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RemoveItem(int id)
        {

            List<ItemVM> lstItem = Helper.AppHelper.GetItemInfoFromSession();
            ItemVM item = lstItem.Where(m => m.Item_Id == id).FirstOrDefault();
            lstItem.Remove(item);
            AppHelper.SaveItemInfoInSession(lstItem);
            return RedirectToAction("Checkout", "Product");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ProductDetails([Bind(Exclude = "Item_Description")]ItemVM item)
        {

            List<ItemVM> lstItem = Helper.AppHelper.GetItemInfoFromSession();
            item.Amount = item.Quantity * item.Item_Price;
            lstItem.Add(item);
            Helper.AppHelper.SaveItemInfoInSession(lstItem);

            return RedirectToAction("Checkout", "Product");





        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ProductDetails(int id)
        {
            ItemVM iVM = new ItemVM();
            iVM = (from i in db.Items.Where(m => m.Item_Id == id)
                   select new ItemVM()
                   {
                       Item_Id = i.Item_Id,
                       Item_Name = i.Item_Name,
                       Item_Price = i.Price,
                       Image_Path = i.Image_Path,
                       Item_Description = i.Item_Description,
                       Category_Id = i.Category_Id,
                       Category_Name = i.Category.Category_Name,
                       Doctor_Report_Path=(from d in db.DoctorReports.Where(a=>a.Item_Id==i.Item_Id) select d.File_Path).FirstOrDefault()                      
                   }).FirstOrDefault();

            iVM.ItemList = (from i in db.Items.Where(m => m.Category_Id == iVM.Category_Id)
                            select new ItemVM()
                            {
                                Item_Id = i.Item_Id,
                                Item_Name = i.Item_Name,
                                Item_Price = i.Price,
                                Image_Path = i.Image_Path,
                                Item_Description = i.Item_Description,
                                Category_Id = i.Category_Id,
                                Category_Name = i.Category.Category_Name

                            }).ToList().SkipWhile(a => a.Item_Id == id).ToList();
            iVM.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/" + "_AfterLoginLayout.cshtml";
            return View(iVM);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Products()
        {
            ItemVM obj = new ItemVM();

            obj.ItemList = (from i in db.Items
                            select new ItemVM()
                            {
                                Item_Id = i.Item_Id,
                                Item_Name = i.Item_Name,
                                Item_Price = i.Price,
                                Image_Path = i.Image_Path,
                                Item_Description = i.Item_Description,
                                Category_Name = i.Category.Category_Name,
                                Data_Filter = i.Category.Data_Filter.Remove(0, 1)

                            }).ToList();
            obj.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/" + "_AfterLoginLayout.cshtml";
            return View(obj);
        }


    }
}