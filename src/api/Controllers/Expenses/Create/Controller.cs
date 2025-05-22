using application.UseCases.Expenses.Create;
using communication.Requests.Expenses;
using communication.Responses;
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
                catch (Exception exc) {
                    var errorResponse = exc switch {
                        ValidationException validation => new ErrorMessage(
                            validation.Errors.Select(e => e.ErrorMessage).ToList()
                        ),
                        ArgumentException exception => new ErrorMessage(
                            [exception.Message]
                        ),
                        _ => new ErrorMessage(
                             ["Unknown error (500) at CreateExpense." ]
                        )
                    };

                    return exc switch {
                        ValidationException => Results.BadRequest(errorResponse),
                        ArgumentException => Results.BadRequest(errorResponse),
                        _ => Results.Problem(
                            detail: string.Join("; ", errorResponse.Errors),
                            statusCode: StatusCodes.Status500InternalServerError
                        )
                    };
                }
            })
            .WithName("CreateExpense")
            .WithTags("Expenses")
            .Produces(StatusCodes.Status201Created)
            .Produces<ErrorMessage>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}