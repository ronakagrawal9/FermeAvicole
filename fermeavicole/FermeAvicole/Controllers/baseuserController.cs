using System.Web.Mvc;
using FermeAvicole.Helper;
using FermeAvicole.ViewModel;
using System.Web.Security;
using System.Web.Routing;

namespace FermeAvicole.Controllers
{
    public class baseuserController : Controller
    {
        protected LoginVM uiDTO;

        public baseuserController()
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
            if (string.IsNullOrEmpty(this.uiDTO.Email_Id))
            {
            //    FormsAuthentication.SignOut();
            //    filterContext.Result =
            //   new RedirectToRouteResult(new RouteValueDictionary
            //     {
            // { "action", "Login" },
            //{ "controller", "Home" },
            //{ "returnUrl", filterContext.HttpContext.Request.RawUrl}
            //      });
                Response.Redirect("~/Admin/login", true);
            }
        }
    }
}