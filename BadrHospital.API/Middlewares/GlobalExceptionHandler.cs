using BadrHospital.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadrHospital.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            // ValidationException carries per-field errors (a dictionary), not a
            // single message - it needs ValidationProblemDetails, a different
            // response shape than the flat ProblemDetails the other branches use.
            if (exception is ValidationException validationException)
            {
                var validationProblemDetails = new ValidationProblemDetails(validationException.Errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more validation errors occurred.",
                    Instance = httpContext.Request.Path
                };

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);
                return true;
            }

            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
                DbUpdateConcurrencyException => (StatusCodes.Status409Conflict, "The record was modified by another user. Please reload and try again."),
                ConflictException => (StatusCodes.Status409Conflict, exception.Message),
                BusinessRuleException => (StatusCodes.Status400BadRequest, exception.Message),
                UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "You do not have permission to perform this action."),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = statusCode == StatusCodes.Status500InternalServerError && !_env.IsDevelopment()
                    ? "An unexpected error occurred. Please contact support if this persists."
                    : exception.Message,
                Instance = httpContext.Request.Path
            };

            // Only leak stack traces / exception type outside prod - never in Production
            if (_env.IsDevelopment())
            {
                problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            }

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
