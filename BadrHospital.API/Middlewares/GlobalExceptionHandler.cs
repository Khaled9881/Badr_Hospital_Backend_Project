using BadrHospital.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

            if (exception is DbUpdateException dbUpdateException &&
                dbUpdateException.InnerException is SqlException sqlException &&
                (sqlException.Number == 2601 || sqlException.Number == 2627))
            {
                var friendlyMessage = ExtractFriendlyConstraintMessage(sqlException.Message);

                var conflictProblem = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "A record with this value already exists.",
                    Detail = friendlyMessage,
                    Instance = httpContext.Request.Path
                };

                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                await httpContext.Response.WriteAsJsonAsync(conflictProblem, cancellationToken);
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

        // Helper Function
        private static string ExtractFriendlyConstraintMessage(string sqlMessage)
        {
            var fieldMap = new Dictionary<string, string>
            {
                ["IX_Patients_PhoneNumber"] = "Phone number is already registered.",
                ["IX_Patients_MedicalRecordNumber"] = "Medical record number already exists.",
                ["UserNameIndex"] = "Username is already taken.",
                ["EmailIndex"] = "Email is already registered.",
                ["IX_Doctors_LicenseNumber"] = "License number is already registered.",
                ["IX_Doctors_Email"] = "Email is already registered.",
                // add more as needed - Pharmacists, LabTechnicians, Invoices.InvoiceNumber, etc.
            };

            foreach (var (indexName, message) in fieldMap)
            {
                if (sqlMessage.Contains(indexName, StringComparison.OrdinalIgnoreCase))
                    return message;
            }

            return "A record with this value already exists.";
        }

    }
}
