using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloService.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HelloService.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!Request.Headers.ContainsKey("api-key")) context.Result = new BadRequestResult();
            var apiKeyValue = Request.Headers["api-key"].ToString();
            if(apiKeyValue != Encryptor.ApiKey()) context.Result = new BadRequestResult();
        }
    }
}