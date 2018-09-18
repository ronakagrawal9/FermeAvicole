using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using FermeAvicole.ViewModel;
using System.Configuration;

namespace FermeAvicole.Helper
{
	public static class AppHelper
	{
		// Get server base url without slash string
		public static string ServerBaseURL
		{
			get
			{
				// return GetAppConfigKeyValue("serverUrl"); 	
				var req = HttpContext.Current.Request;
				return string.Format("{0}://{1}{2}", req.Url.Scheme, req.Url.Authority, HttpRuntime.AppDomainAppVirtualPath).TrimEnd('/') + "/";
			}
		}

        public enum enumRoleType
        {
            Admin = 1,
            Employee = 2,
            Customer = 3
        }

       

        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
        public static MvcHtmlString CommaSeparated(this HtmlHelper helper, IEnumerable<MvcHtmlString> list)
        {
            return new MvcHtmlString(string.Join(", ", list));
        }
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["AxiomConnectionString"].ConnectionString;                
            }
        }

        public static void SaveItemInfoInSession(List<ItemVM> lstItem)
        {
            HttpContext.Current.Session["ItemList"] = lstItem;
        }

        public static List<ItemVM> GetItemInfoFromSession()
        {
            List<ItemVM> lstItem;

           
            if (HttpContext.Current.Session["ItemList"] == null)
            {
                lstItem = new List<ItemVM>();
                SaveItemInfoInSession(lstItem);
            }
            else
            {
                lstItem = (List<ItemVM>)HttpContext.Current.Session["ItemList"];
            }
            return lstItem;
        }
        public static string ApplicationPath
        {
            // Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location).FullName
            // get { return AppDomain.CurrentDomain.BaseDirectory; }
            get { return HttpContext.Current.Server.MapPath("~"); }// +@"\"; }
        }
        public static void SaveUserInfoDTOInSession(LoginVM uDTO)
        {
            HttpContext.Current.Session["G_UserInfoDTO"] = uDTO;
        }


        public static LoginVM GetUserInfoDTOFromSession()
        {
            LoginVM uiDTO;

            // When session for UDInfoDTO is null, i.e. first page visit or session get expired
            if (HttpContext.Current.Session["G_UserInfoDTO"] == null)
            {
                uiDTO = new LoginVM();
                SaveUserInfoDTOInSession(uiDTO);
            }
            else
            {
                uiDTO = (LoginVM)HttpContext.Current.Session["G_UserInfoDTO"];
            }
            return uiDTO;
        }

     
        
       
	}
}