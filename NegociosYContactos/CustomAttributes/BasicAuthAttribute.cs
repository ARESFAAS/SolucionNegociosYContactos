﻿using NegociosYContactos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace NegociosYContactos.CustomAttributes
{
    public class BasicAuth : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
           
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserAuthenticated"] != null)
            {
                var user = (User)filterContext.HttpContext.Session["UserAuthenticated"];
                if (!user.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
