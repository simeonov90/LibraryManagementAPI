using LibraryManagement.API.Models;
using LibraryManagement.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace LibraryManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse();

            switch (exception)
            {
                case ValidationException validationEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = validationEx.Message;
                    break;
                case NotFoundException notFound:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = notFound.Message;
                    break;
                case BadRequestException badRequest:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = badRequest.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "An unexpected error occurred.";
                    break;
            }

            errorResponse.StatusCode = response.StatusCode;
            await response.WriteAsJsonAsync(errorResponse);
        }
    }
}
