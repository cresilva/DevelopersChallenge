using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nibo.SpaceJam.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.WebAPI
{
    public class ArgumentExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var result = default(object);

            if (context.Exception is ArgumentException)
                result = new { context.Exception.Message, ((ArgumentException)context.Exception).ParamName };
            
            else if (context.Exception is ArgumentNullException)
                result = new { context.Exception.Message, ((ArgumentNullException)context.Exception).ParamName };

            context.Result = new BadRequestObjectResult(result);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var result = default(object);

            if (context.Exception is ArgumentException)
                result = new { context.Exception.Message, ((ArgumentException)context.Exception).ParamName };

            else if (context.Exception is ArgumentNullException)
                result = new { context.Exception.Message, ((ArgumentNullException)context.Exception).ParamName };

            context.Result = new BadRequestObjectResult(result);

            return Task.CompletedTask;
        }

    }
}
