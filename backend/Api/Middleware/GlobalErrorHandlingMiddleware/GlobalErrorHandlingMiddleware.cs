using System.Net;
using System.Security.Authentication;
using Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api.Middleware.GlobalErrorHandlingMiddleware;

public class GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
{
	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
		catch (Exception e)
		{
			logger.LogError(e, "An error occurred: {Message}", e.Message);
			await HandleExceptionAsync(httpContext, e);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		HttpStatusCode status;
		string message;

		switch (exception)
		{
			case DirectoryNotFoundException:
			case FileNotFoundException:
			case KeyNotFoundException:
			case NotFoundException:
				status = HttpStatusCode.NotFound;
				message = "The requested resource was not found.";
				break;

			case NotImplementedException:
				status = HttpStatusCode.NotImplemented;
				message = "This feature is not yet implemented.";
				break;

			case UnauthorizedAccessException:
			case AuthenticationException:
				status = HttpStatusCode.Unauthorized;
				message = "Unauthorized access.";
				break;

			case ArgumentNullException:
			case ArgumentOutOfRangeException:
			case ArgumentException:
				status = HttpStatusCode.BadRequest;
				message = "Invalid input provided.";
				break;
			case DbUpdateConcurrencyException:
				status = HttpStatusCode.Conflict;
				message = "A concurrency conflict occurred during update.";
				break;

			case DbUpdateException:
				status = HttpStatusCode.BadRequest;
				message = "A database update error occurred.";
				break;

			default:
				status = HttpStatusCode.InternalServerError;
				message = "An unexpected error occurred. Please try again later.";
				break;
		}

		var response = new { error = message };
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)status;

		return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
	}
}