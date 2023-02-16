using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Common.Models.Authentification.interfaces;

namespace TripmateApi.Common.Authentification
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = (IInternalUser)context.HttpContext.Items["User"];
            if (token == null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new JsonResult(Envelope.Error("User is not well authenticated"));
            }
        }
    }
}
