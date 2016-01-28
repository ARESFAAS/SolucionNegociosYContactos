using NegociosYContactos.Models;
using System.Web.Mvc;
//using System.Web.Mvc.Filters;
using System;

namespace NegociosYContactos.CustomAttributes
{
    public class BasicAuth : ActionFilterAttribute, IAuthorizationFilter
    {
        //public void OnAuthentication(AuthenticationContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session["UserAuthenticated"] != null)
        //    {
        //        var user = (User)filterContext.HttpContext.Session["UserAuthenticated"];
        //        if (!user.IsAuthenticated)
        //        {
        //            filterContext.Result = new RedirectResult("~/Home/Index");
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/Home/Index");
        //    }
        //}

        //public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session["UserAuthenticated"] != null)
        //    {
        //        var user = (User)filterContext.HttpContext.Session["UserAuthenticated"];
        //        if (!user.IsAuthenticated)
        //        {
        //            filterContext.Result = new RedirectResult("~/Home/Index");
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectResult("~/Home/Index");
        //    }
        //}

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserAuthenticated"] != null)
            {
                var user = (User)filterContext.HttpContext.Session["UserAuthenticated"];
                if (!user.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}
