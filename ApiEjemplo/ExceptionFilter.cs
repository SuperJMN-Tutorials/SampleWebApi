using BikingUltimate.Server.Features.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BikingUltimate.Server
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException ex)
            {
                context.Result = new NotFoundObjectResult(ex.Message);
            }
        }
    }
}