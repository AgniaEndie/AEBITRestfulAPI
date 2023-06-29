using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AEBITRestfulAPI.Filters
{
    public class HideAuthenticationFilter : ActionFilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var check = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (check != "")
            {
                context.Result = new NoContentResult();
            }
        }
    }
}
