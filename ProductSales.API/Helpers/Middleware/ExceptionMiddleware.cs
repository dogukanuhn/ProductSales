using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductSales.Domain;
using ProductSales.Domain.Concrete;
using ProductSales.Domain.Exceptions;
using ProductSales.Infrastructure.Error;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace ProductSales.API.Helpers.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) when (ex is NotFoundException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                httpContext.Response.ContentType = "json/application";
                Log.Warning(ex, "Not Found");
                var error = new ErrorModel
                {
                    Code = "404",
                    Message = ex.Message
                };
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is DomainException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                httpContext.Response.ContentType = "json/application";
                Log.Error(ex, "Conflict");
                var error = new ErrorModel
                {
                    Code = "409",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is AuthenticationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                httpContext.Response.ContentType = "json/application";
                Log.Error(ex, "Authentication Problem");
                var error = new ErrorModel
                {
                    Code = "403",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is PaymentException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                httpContext.Response.ContentType = "json/application";
                Log.Error(ex, "Payment Problem");
                var error = new ErrorModel
                {
                    Code = "406",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is ApplicationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                httpContext.Response.ContentType = "json/application";
                Log.Error(ex, "NotAcceptable");
                var error = new ErrorModel
                {
                    Code = "00",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex) when (ex is ValidationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "json/application";
                Log.Error(ex, "BadRequest");
                var error = new ErrorModel
                {
                    Code = "400",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "json/application";
                Log.Error(ex, "Error");
                var error = new ErrorModel
                {
                    Code = "00",
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }
    }
}
