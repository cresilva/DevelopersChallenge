using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nibo.SpaceJam.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.WebAPI
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
                context.Result = new BadRequestObjectResult(this.GetPayload((ValidationException)context.Exception));
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
                context.Result = new BadRequestObjectResult(this.GetPayload((ValidationException)context.Exception));

            return Task.CompletedTask;
        }

        private object GetPayload(ValidationException exception)
        {
            return new
            {
                exception.Message,
                exception.Errors
            };
        }
    }
}
