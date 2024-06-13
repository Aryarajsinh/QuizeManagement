using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace QuizeManagement.Helpers.Helper
{
    public class AdminAuthorizeAttribute :AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            QuizeManagement_0406Entities _option = new QuizeManagement_0406Entities();
            RegisterModel db = new RegisterModel();

            Admin_Table _RegistersUserUsername = ConvertModelHelper.convertRegisterModelToRegister(db);
            _RegistersUserUsername = _option.Admin_Table.FirstOrDefault(m => m.Username == SessionHelper.LoggedInUser);


            if (_RegistersUserUsername != null)
            {
                return SessionHelper.IsUserLoggedIn;
            }

            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!SessionHelper.IsUserLoggedIn)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            else
            {     // Not Direct Acess Home/Index Using This Methods 

                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }
    }
}
