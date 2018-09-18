using FermeAvicole.Helper;
using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FermeAvicole.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();

        }

        [HttpPost]

        [AllowAnonymous]
        public ActionResult Login(LoginVM user)
        {


            if (ModelState.IsValid)
            {
                try
                {

                    Registration r;
                    r = db.Registrations.Where(m => m.Email_Id == user.Email_Id && m.Password == user.Password && m.Role_Id == 1).FirstOrDefault();
                    if (r != null)
                    {
                        user.UserID = r.User_Id;
                        user.Role_Id = Convert.ToInt32(AppHelper.enumRoleType.Customer);
                        user.Email_Id = r.Email_Id;
                        user.First_Name = r.First_Name;
                        user.Last_Name = r.Last_Name;
                        AppHelper.SaveUserInfoDTOInSession(user);
                        FormsAuthentication.SetAuthCookie(user.Email_Id, user.RememberMe);

                        return RedirectToAction("Dashboard", "admin");

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
                    return View("Error", new HandleErrorInfo(ex, "Admin", "Login"));

                }

            }

            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }

        



    }
}