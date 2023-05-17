using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;

namespace WBAV6.Helpers
{
    public class ValidationHelper : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObjectFromJson<User>("user") == null)
            {
               context.Result = new RedirectResult("~/Login/Signin");
            }
            //await next();
            base.OnActionExecuting(context);
        }
    }
}
