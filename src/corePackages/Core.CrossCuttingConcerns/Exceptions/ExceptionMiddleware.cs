using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object errors = null;

            if(exception.GetType() == typeof(ValidationException))
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                errors = ((ValidationException)exception).Errors;

                return context.Response.WriteAsync(new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://example.com/probs/validation",
                    Title = "Validation error(s)",
                    Detail = (errors as IEnumerable<ValidationFailure>)?.FirstOrDefault()?.ToString(), 
                    Instance = "",
                    Errors = errors
                }.ToString() );
            }

            if (exception.GetType() == typeof(BusinessException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return context.Response.WriteAsync(new BusinessProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://example.com/probs/business",
                    Title = "Business exception",
                    Detail = exception.Message,
                    Instance = ""
                }.ToString());
            }

            var unknownException = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://example.com/probs/internal",
                Title = "Internal exception",
                Detail = exception.Message,
                Instance = ""
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(unknownException));
        }
    }
}
