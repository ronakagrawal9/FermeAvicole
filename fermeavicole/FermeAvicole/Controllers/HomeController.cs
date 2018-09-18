using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FermeAvicole.ViewModel;
using System.IO;
using System.Web.Security;
using FermeAvicole.Helper;


namespace FermeAvicole.Controllers
{
    [Authorize]
    public class HomeController : baseCustomerController
    {
        // GET: Home

        FermeDBEntities db = new FermeDBEntities();
    
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            ItemVM obj = new ItemVM();
           
            obj.CateGoryList = (from c in db.Categories
                                select new CategoryVM()
                                {
                                    Category_Id = c.Category_Id,
                                    Category_Name = c.Category_Name,
                                    Data_Filter = c.Data_Filter
                                }).ToList();

            obj.ItemList= (from i in db.Items
                           select new ItemVM()
                           {
                               Item_Id = i.Item_Id,
                               Item_Name = i.Item_Name,
                               Item_Price = i.Price,
                               Image_Path = i.Image_Path,
                               Item_Description = i.Item_Description,
                               Category_Name = i.Category.Category_Name,
                               Data_Filter=i.Category.Data_Filter.Remove(0,1)

                           }).ToList();
            obj.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/"+ "_AfterLoginLayout.cshtml";
            return View(obj);
        }

        [HttpGet]
        public ActionResult MyOrders()
        {
            int user_Id = AppHelper.GetUserInfoDTOFromSession().UserID;
            List<OrderVM> ViewOrderList = new List<OrderVM>();
            ViewOrderList = (from o in db.Orders.Where(m => m.User_Id == user_Id)
                             select new OrderVM()
                             {
                                 Order_Id = o.Order_Id,
                                 User_Id = o.User_Id,
                                 Date = o.Date,
                                 Total_Amount = o.Total_Amount,
                                 OrderStatus_Id = o.OrderStatus_Id,
                                 Order_Status = o.OrderStatu.Order_Status,
                                 Customer_Name = o.Registration.First_Name
                             }).ToList();
            return View(ViewOrderList);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            int userid = Helper.AppHelper.GetUserInfoDTOFromSession().UserID;
            RegistrationVM obj = (from r in db.Registrations.Where(m => m.User_Id == userid)
                                  select new RegistrationVM()
                                  {
                                      User_Id = r.User_Id,
                                      Role_Id = r.Role_Id,
                                      First_Name = r.First_Name,
                                      Last_Name = r.Last_Name,
                                      Contact_Number = r.Contact_Number,
                                      Image_Path = r.Image_Path,
                                      Address = r.Address,
                                      PinCode = r.PinCode
                                  }).FirstOrDefault();
            return View(obj);
        }
        [HttpPost]
        public ActionResult MyProfile(RegistrationVM regstration)
        {
            string filExtension = string.Empty;
            if (regstration.ImageFile != null)
            {
                Guid fileGuid = Guid.NewGuid();
                filExtension = Path.GetExtension(regstration.ImageFile.FileName);
                string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                regstration.ImageFile.SaveAs(path);
                regstration.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
            }
            Registration r = db.Registrations.Where(m => m.User_Id == regstration.User_Id).FirstOrDefault();
            r.First_Name = regstration.First_Name;
            r.Last_Name = regstration.Last_Name;
            r.Contact_Number = regstration.Contact_Number;
            r.Image_Path = regstration.Image_Path;
            r.Address = regstration.Address;
            r.PinCode = regstration.PinCode;
            db.SaveChanges();
            return RedirectToAction("MyProfile","Home");
        }

        [HttpGet]
        public ActionResult ViewOrder(int id)
        {
            OrderVM order = new OrderVM();


            order.OrderItems = (from ot in db.OrderTransactions.Where(m => m.Order_Id == id)
                                select new OrderTransactionVM()
                                {
                                    OrderTransaction_Id = ot.Order_Id,
                                    Order_Id = ot.Order_Id,
                                    Item_Id = ot.Item_Id,
                                    Item_Name = ot.Item.Item_Name,
                                    Quantity = ot.Quantity,
                                    Amount = ot.Amount,
                                    Total = ot.Total,

                                }).ToList();

            order.PaymentTransaction = (from pt in db.PaymentTransactions.Where(m => m.Order_Id == id)
                                        select new PaymentTransactionVM()
                                        {
                                            PaymentTransaction_Id = pt.PaymentTransaction_Id,
                                            Date = pt.Date,
                                            Order_Id = pt.Order_Id,
                                            User_Id = pt.User_Id,
                                            Amount = pt.Amount,
                                            Payment_Mode = pt.Payment_Mode,

                                        }).FirstOrDefault();



            return View(order);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/" + "_AfterLoginLayout.cshtml";
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult AboutUS()
        {
            ViewBag.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/" + "_AfterLoginLayout.cshtml";
            return View();
        }


      
        [AllowAnonymous]
        public ActionResult Blog()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult OurTeam()
        {
            ViewBag.Layout = string.IsNullOrEmpty(AppHelper.GetUserInfoDTOFromSession().Email_Id) ? "~/Views/Shared/_Layout.cshtml" : "~/Views/Shared/" + "_AfterLoginLayout.cshtml";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Products()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Payment()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.DDLRole = GetDDLRole();
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                TempData["returnUrl"] = returnUrl;
            }
            return View();

        }

        [HttpPost]
     
        [AllowAnonymous]
        public ActionResult Login(LoginVM user)
        {

            user.Role_Id = 3;
            if (ModelState.IsValid)
            {
                try
                {

                    Registration r;
                    r = db.Registrations.Where(m => m.Email_Id == user.Email_Id && m.Password == user.Password  && m.Role_Id==user.Role_Id).FirstOrDefault();
                    if (r != null)
                    {
                        user.UserID = r.User_Id;
                        user.Role_Id = Convert.ToInt32(AppHelper.enumRoleType.Customer);
                        user.Email_Id = r.Email_Id;
                        user.First_Name = r.First_Name;
                        user.Last_Name = r.Last_Name;
                       AppHelper.SaveUserInfoDTOInSession(user);
                        FormsAuthentication.SetAuthCookie(user.Email_Id, user.RememberMe);
                        if (TempData["returnUrl"] != null && !string.IsNullOrWhiteSpace(TempData["returnUrl"].ToString()))
                            return Redirect(TempData["returnUrl"].ToString());
                        else
                            return RedirectToAction("Index", "Home");


                    }              
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Email or Password entered");
                        ViewBag.Error = true;
                        return View(user);
                    }
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "home", "Login"));

                }

            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "home");
        }

        [AllowAnonymous]
        public List<SelectListItem> GetDDLRole()
        {
            List<SelectListItem> lstRole = new List<SelectListItem>();
            var data = db.Roles.ToList();
            foreach (var item in data)
            {
                lstRole.Add(new SelectListItem()
                {
                    Text = item.Role_Name,
                    Value = item.Role_Id.ToString(),
                    Selected = item.Role_Id == 0
                });
            }
            return lstRole;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            string returnUrl = TempData.Peek("returnUrl") !=null ? TempData.Peek("returnUrl").ToString() : string.Empty;
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                TempData["returnUrl"] = returnUrl;
            }
            ViewBag.DDLRole = GetDDLRole();
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            switch(Convert.ToInt32(TempData["Role_Id"]))
            {
                case 1:
                    ViewBag.Message = "Employee account has been created successfully";
                    break;

                case 2:
                    ViewBag.Message = "Admin account has been created successfully";
                    break;

                case 3:
                    ViewBag.Message = "Customer account has been created successfully";
                    break;

            }
            



            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegistrationVM obj)
        {

            string filExtension = "";
            try
            {
                if (ModelState.IsValid)
                {
                             
                    if (obj.ImageFile.FileName!=null)
                    {
                        Guid fileGuid = Guid.NewGuid();
                        filExtension = Path.GetExtension(obj.ImageFile.FileName);
                        string path = Server.MapPath("~/Upload/" + fileGuid.ToString() + filExtension);
                        obj.ImageFile.SaveAs(path);
                        obj.Image_Path = "/Upload/" + fileGuid.ToString() + filExtension;// path to store file in database
                    }
                    else
                    {

                        obj.Image_Path = "~/Upload/" + "Default.png";

                    }
                    Registration r = new Registration();
                    r.First_Name = obj.First_Name;
                    r.Last_Name = obj.Last_Name;
                    r.Email_Id = obj.Email_Id;
                    r.Password = obj.Password;
                    r.Address = obj.Address; ;
                    r.PinCode = obj.PinCode;  
                    r.Contact_Number = obj.Contact_Number;
                    r.Role_Id = Convert.ToInt32(AppHelper.enumRoleType.Customer);
                    r.Image_Path = obj.Image_Path;
                    db.Registrations.Add(r);
                    db.SaveChanges();
                    TempData["Role_Id"] = obj.Role_Id;
                    LoginVM user = new LoginVM();
                    user.UserID = r.User_Id;
                    user.Role_Id = Convert.ToInt32(AppHelper.enumRoleType.Customer);
                    user.Email_Id = r.Email_Id;
                    user.First_Name = r.First_Name;
                    user.Last_Name = r.Last_Name;
                    AppHelper.SaveUserInfoDTOInSession(user);
                    FormsAuthentication.SetAuthCookie(user.Email_Id, user.RememberMe);

                    if (TempData["returnUrl"] != null && !string.IsNullOrWhiteSpace(TempData["returnUrl"].ToString()))
                        return Redirect(TempData["returnUrl"].ToString());
                    else
                        
                    return RedirectToAction("RegisterSuccess", "Home");
                }
                else
                {
                    ViewBag.DDLRole = GetDDLRole();
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.DDLRole = GetDDLRole();
                return View(ex.Message);
            }
           
                
        }
    }
}