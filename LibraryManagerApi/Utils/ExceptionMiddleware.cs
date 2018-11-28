using System;
using System.Threading.Tasks;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LibraryManagerApi.Middleware
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
            context.Response.ContentType = "application/json";
            switch (ex)
            {
                case EntityNotFoundException notFoundException:
                    return HandleNotFound(context, notFoundException);
                case ArgumentException argumentException:
                    return HandleArgumentException(context, argumentException);
            }
            return HandleException(context);
        }

        private Task HandleException(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context
                .Response
                .WriteAsync(SerializeErrorMessage("Internal server error."));                
        }

        private Task HandleArgumentException(HttpContext context, ArgumentException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context
                .Response
                .WriteAsync(SerializeErrorMessage(ex.Message));                
        }

        private Task HandleNotFound(HttpContext context, EntityNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return context
                .Response
                .WriteAsync(SerializeErrorMessage(ex.Message));
        }

        private string SerializeErrorMessage(string message)
        {
            return JsonConvert.SerializeObject(new { message });
        }
    }
}