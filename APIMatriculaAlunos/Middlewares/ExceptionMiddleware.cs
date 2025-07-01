using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using APIMatriculaAlunos.Middlewares.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace APIMatriculaAlunos.NewFolder
{
    public class ExceptionMiddleware:IExceptionFilter
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MyApiException)
                HandleProjectException(context);
            else
                ThrowUnknowException(context);
        }
        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is InvalidLoginException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new { context.Exception.Message });
            }
            else if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new { Message = exception!.ErrorMessages });
            }

        }
        private void ThrowUnknowException(ExceptionContext context)  
        {
            _logger.LogError($"EXCEPTION!!! {context.Exception.Message}");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new {Message= "Unknown Error" });
        }
    }
}
