using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class AuthSessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}