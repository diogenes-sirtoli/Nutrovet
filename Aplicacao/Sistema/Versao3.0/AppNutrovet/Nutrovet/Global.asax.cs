using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Nutrovet
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Application["UsersLoggedIn"] = new List<string>();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //string userLoggedIn = (Session["UserLoggedIn"] == null ? string.Empty :
            //    (string)Session["UserLoggedIn"]);

            //if (userLoggedIn.Length > 0)
            //{
            //    List<string> d = Application["UsersLoggedIn"] as List<string>;

            //    if (d != null)
            //    {
            //        lock (d)
            //        {
            //            d.Remove(userLoggedIn);
            //        }
            //    }
            //}
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}