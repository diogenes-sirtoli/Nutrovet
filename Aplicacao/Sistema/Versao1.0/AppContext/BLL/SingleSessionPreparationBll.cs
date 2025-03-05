using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace BLL
{
    internal static class SingleSessionPreparationBll
    {
        /// <summary>
        /// Called during LoggedIn event. Need to pass username
        /// as login process not fully completed
        /// </summary>
        internal static void CreateAndStoreSessionToken(string userName)
        {
            // Will be using the response object several times
            HttpResponse pageResponse = HttpContext.Current.Response;

            // 'session' token
            Guid sessionToken = Guid.NewGuid();

            // Get authentication cookie and ticket
            HttpCookie authenticationCookie =
                pageResponse.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authenticationTicket =
                FormsAuthentication.Decrypt(authenticationCookie.Value);

            // Create a new ticket based on the existing one that includes the 'session' token in the userData
            FormsAuthenticationTicket newAuthenticationTicket =
                new FormsAuthenticationTicket(
                authenticationTicket.Version,
                authenticationTicket.Name,
                authenticationTicket.IssueDate,
                authenticationTicket.Expiration,
                authenticationTicket.IsPersistent,
                sessionToken.ToString(),
                authenticationTicket.CookiePath);

            // Store session token in Membership comment
            // You may want to store other information in the comment
            // field, if so, you may have to implement some dilimited
            // structure within it, perhaps xml
            MembershipUser currentUser = Membership.GetUser(userName);
            currentUser.Comment = sessionToken.ToString();
            Membership.UpdateUser(currentUser);

            // Replace the authentication cookie
            pageResponse.Cookies.Remove(FormsAuthentication.FormsCookieName);

            HttpCookie newAuthenticationCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(newAuthenticationTicket));

            newAuthenticationCookie.HttpOnly = authenticationCookie.HttpOnly;
            newAuthenticationCookie.Path = authenticationCookie.Path;
            newAuthenticationCookie.Secure = authenticationCookie.Secure;
            newAuthenticationCookie.Domain = authenticationCookie.Domain;
            newAuthenticationCookie.Expires = authenticationCookie.Expires;

            pageResponse.Cookies.Add(newAuthenticationCookie);
        }
    }
}
