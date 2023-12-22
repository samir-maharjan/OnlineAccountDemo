using OnlineAccountDemo.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace OnlineAccountDemo.CustomAttributes
{
    public class GeneralAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = SessionService.GetSession(context.HttpContext);
            bool isLogged = user != null;
            if (!isLogged)
            {
                context.Result = new RedirectResult("/Home/Index");
                return;
            }
        }
        
    }
    public class AdminAuthorizationAttribute: GeneralAuthorizationAttribute, IAuthorizationFilter
    {
        public new void OnAuthorization(AuthorizationFilterContext context)
        {
            base.OnAuthorization(context);
            var user = SessionService.GetSession(context.HttpContext);

        } 
    }
    public class UserAuthorizationAttribute: Attribute, IAuthorizationFilter
    {
        public new void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = SessionService.GetSession(context.HttpContext);

        }
    }
}
