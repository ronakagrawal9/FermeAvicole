using System.Web.Mvc;
using FermeAvicole.Helper;
using FermeAvicole.ViewModel;
using System.Web.Security;
using System.Web.Routing;

namespace FermeAvicole.Controllers
{
    public class baseCustomerController : Controller
    {
        protected LoginVM uiDTO;
     
        public baseCustomerController()
        {
            this.uiDTO = AppHelper.GetUserInfoDTOFromSession();
        }

        // <summary>
        /// This method will get executed before, its child class controler action method get called.
        /// Validate user is logged in or not
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.ActionDescriptor.ActionName == "MakePaymentOnline" || filterContext.ActionDescriptor.ActionName == "MakePaymentCash")
            {
                if (string.IsNullOrEmpty(this.uiDTO.Email_Id))
                {

                    filterContext.Result =
                   new RedirectToRouteResult(new RouteValueDictionary
                     {
             { "action", "Login" },
            { "controller", "Home" },
            { "returnUrl", filterContext.HttpContext.Request.RawUrl}
                      });
                    //  Response.Redirect("~/home/login", true);
                    //  RedirectToAction("login", "home", new { returnUrl= filterContext.HttpContext.Request.RawUrl });
                }
            }
           
        }
    }
}