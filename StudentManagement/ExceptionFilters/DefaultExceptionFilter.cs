using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentManagement.Exceptions;
using System.IO;
using System.Net;
using System.Text;

namespace StudentManagement.ExceptionFilters
{
    public class DefaultExceptionFilter : IExceptionFilter
    {
        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
        // https://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
        // https://www.c-sharpcorner.com/blogs/global-exception-handling-in-asp-net-core-as-exception-filter

        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var message = "Error happens in server.";
            if (context.Exception is InvalidParameterException)
            {
                status = HttpStatusCode.BadRequest;
                message = context.Exception.Message;
            }
            else if (context.Exception is ConflictException)
            {
                status = HttpStatusCode.Conflict;
                message = context.Exception.Message;
            }
            else if (context.Exception is NotFoundException)
            {
                status = HttpStatusCode.NotFound;
                message = context.Exception.Message;
            }
            else if (context.Exception is DbUpdateException)
            {
                status = HttpStatusCode.BadRequest;
                message = "Invalid parameters."; //TODO log
            }
            else
            {
                //TODO log error
            }

            var result = new { message = message };
            var json = JsonConvert.SerializeObject(result);
            var bytes = Encoding.UTF8.GetBytes(json);

            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            response.ContentLength = bytes.Length;
            var asyncResult = response.BodyWriter.WriteAsync(bytes).Result;
        }
    }
}
