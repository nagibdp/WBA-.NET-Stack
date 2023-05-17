using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;

namespace WBAV6.Helpers
{
    public class RoleHelper : ActionFilterAttribute
    {
        private readonly string _role;

        public RoleHelper(string role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObjectFromJson<User>("user").Role != _role)
            {
                context.Result = new RedirectResult("~/Home");
            }
            base.OnActionExecuting(context);
        }
    }
}
