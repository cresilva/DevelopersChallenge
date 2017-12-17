using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nibo.SpaceJam.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.SpaceJam.WebAPI
{
    public class NotFoundExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
                context.Result = new NotFoundObjectResult(context.Exception.Message);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
                context.Result = new NotFoundObjectResult(context.Exception.Message);

            return Task.CompletedTask;
        }
    }
}
