using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using APIMatriculaAlunos.Middlewares.Exceptions;
using APIMatriculaAlunos.Entities.Dtos;

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
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
            else if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }
            else if (context.Exception is UserNotOwnerException)
            {
                var exception = context.Exception as UserNotOwnerException;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.Message ));
            }

        }
        private void ThrowUnknowException(ExceptionContext context)  
        {
            _logger.LogError($"EXCEPTION! {context.Exception.Message}");

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson ("Internal server error"));
        }
    }
}
