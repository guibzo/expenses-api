using application.UseCases.Expenses.Create;
using communication.Requests.Expenses;
using communication.Responses;
using exception.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Expenses.Create;

public static class CreateExpenseEndpoint {
    public static void MapCreateExpense(this IEndpointRouteBuilder app) {
        app.MapPost("/expenses", ([FromBody] CreateExpenseRequest request) => {
                CreateExpenseUseCase useCase = new();
                
                try {
                    useCase.Execute(request);
                    return Results.Created();
                }
                catch (Exception exception) {
                    var errorMessages = exception switch {
                        CustomValidationException validation => new ErrorMessages(validation.ErrorMessages),
                        ArgumentException argException => new ErrorMessages(
                            [argException.Message]
                        ),
                        _ => new ErrorMessages(
                             ["Unknown error (500) at CreateExpense." ]
                        )
                    };

                    return exception switch {
                        CustomValidationException => Results.BadRequest(errorMessages),
                        ArgumentException => Results.BadRequest(errorMessages),
                        _ => Results.Problem(
                            detail: string.Join("; ", errorMessages.Errors),
                            statusCode: StatusCodes.Status500InternalServerError
                        )
                    };
                }
            })
            .WithName("CreateExpense")
            .WithTags("Expenses")
            .Produces(StatusCodes.Status201Created)
            .Produces<ErrorMessages>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}