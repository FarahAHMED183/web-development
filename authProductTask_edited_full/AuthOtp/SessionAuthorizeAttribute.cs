using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthOtp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _tokenHeader;
        public SessionAuthorizeAttribute(string tokenHeader = "X-Session-Token")
        {
            _tokenHeader = tokenHeader;
        }

        public async System.Threading.Tasks.Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var req = context.HttpContext.Request;
            if (!req.Headers.ContainsKey(_tokenHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = req.Headers[_tokenHeader].ToString();
            var otpService = (IOtpService)context.HttpContext.RequestServices.GetService(typeof(IOtpService));
            if (otpService == null)
            {
                context.Result = new StatusCodeResult(500);
                return;
            }

            var (ok, userId) = otpService.ValidateSessionToken(token);
            if (!ok)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // attach user id to HttpContext for controller access
            context.HttpContext.Items["session_user"] = userId;
            await next();
        }
    }
}
