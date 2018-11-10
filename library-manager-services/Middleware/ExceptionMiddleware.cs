using System;
using System.Threading.Tasks;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LibraryManager.Middleware
{
    public class ExceptionMiddeware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddeware(RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            switch (ex)
            {
                case EntityNotFoundException<IEntity> notFoundException:
                    return HandleNotFound(context, notFoundException);
                case ArgumentException argumentException:
                    return HandleArgumentException(context, argumentException);
            }
            return HandleException(context);
        }

        private static Task HandleException(HttpContext context)
        {
            return context
                .Response
                .WriteAsync(new ErrorDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error."
                }.ToString());
        }

        private static Task HandleArgumentException(HttpContext context, ArgumentException argumentException)
        {
            return context
                .Response
                .WriteAsync(new ErrorDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = argumentException.Message
                }.ToString());
        }

        private static Task HandleNotFound(HttpContext context, EntityNotFoundException<IEntity> notFoundException)
        {
            return context
                .Response
                .WriteAsync(new ErrorDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = notFoundException.Message
                }.ToString());
        }
    }
}