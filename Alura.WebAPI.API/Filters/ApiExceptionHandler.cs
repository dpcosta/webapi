using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.API.Filters
{
    public static class ExceptionHelper
    {
        public static object RetornaErro(this Exception exception)
        {
            return new
            {
                Error = new
                {
                    code = exception.HResult,
                    message = exception.Message
                }
            };
        }
    }

    public class ApiExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var result = new ObjectResult(context.Exception.RetornaErro());
            result.StatusCode = 500;
            context.Result = result;
        }
    }
}
