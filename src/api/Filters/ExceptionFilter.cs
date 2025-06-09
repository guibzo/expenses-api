using communication.Responses;
using exception.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Filters;

public class ExceptionHandlerFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        try {
            return await next(context);
        }
        catch (Exception exception) {
            if (exception is AppException appException) return HandleException(appException);
            return HandleUnknownError();
        }
    }

    private IResult HandleException(AppException exception) {
        var errorMessages = new ErrorMessages(exception.GetErrors());

        var resultException = exception.StatusCode switch {
            StatusCodes.Status400BadRequest => Results.BadRequest(errorMessages),
            StatusCodes.Status404NotFound => Results.NotFound(errorMessages),
            StatusCodes.Status409Conflict => Results.Conflict(errorMessages),
            _ => Results.Problem(
                detail: string.Join("; ", errorMessages.Errors),
                statusCode: exception.StatusCode
            )
        };

        return resultException;
    }
    
    private IResult HandleUnknownError() {
        return Results.Problem(
            detail: "Unknown error (500).",
            statusCode: 500
        );
    }
}