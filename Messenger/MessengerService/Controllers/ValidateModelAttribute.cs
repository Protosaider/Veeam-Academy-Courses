using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MessengerService.Controllers
{
    /// <summary>
    /// Returns 400 (BadRequest) if the ModelState is invalid.
    /// It helps not to write every single time this:
    /// <code>
    /// if (!ModelState.IsValid)
    ///     return BadRequest(ModelState);
    /// </code>  
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	internal sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
                return;

            actionContext.Response =
                actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
        }
    }
}