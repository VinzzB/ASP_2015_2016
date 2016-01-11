using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace Asp_Mvc_2015_2016.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureString = null;
            //get from cookie first. If no cookie defined, get from request...
            HttpCookie cookie = Request.Cookies["culture"];
            cultureString = cookie != null ? cookie.Value : (Request.UserLanguages[0] ?? "nl-BE");
            //set culture on current user thread.
            try
            {           
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureString);
            }
            catch (Exception)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-BE");                
            }
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

         [AllowAnonymous]
        [HttpPost]
        public ActionResult SetCulture(string culture, string returnaction, string returnId)
        {
            //get cookie from request. if cookie is null (??) then create new cookie and init props.
            HttpCookie cookie = Request.Cookies["culture"] ?? new HttpCookie("culture") { Expires = DateTime.Now.AddYears(1) };
            cookie.Value = culture;         //set cookie value.
            Response.Cookies.Add(cookie);   //add cookie to http response and redirect to action
            return RedirectToAction(returnaction, new { id = returnId });
        }
    }
}