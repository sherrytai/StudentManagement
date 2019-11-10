using Microsoft.AspNetCore.Mvc.Filters;
using StudentManagement.Exceptions;
using System.Net;

namespace StudentManagement.ExceptionFilters
{
    public class DefaultExceptionFilter : IExceptionFilter
    {
        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
        // https://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/

        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var message = "Error happens in server.";
            var isHandled = false;
            if (context.Exception is InvalidParameterException)
            {
                //context.Result
            }

           
        }
    }
}
