using application.UseCases.Expenses.Create;
using communication.Requests.Expenses;
using communication.Responses;
using Microsoft.AspNetCore.Mvc;
using api.Filters;

namespace api.Controllers.Expenses.Create;

public static class CreateExpenseEndpoint {
    public static void MapCreateExpense(this IEndpointRouteBuilder app) {
        app.MapPost("/expenses", ([FromBody] CreateExpenseRequest request) => {
                CreateExpenseUseCase useCase = new();
                useCase.Execute(request);
                return Results.Created();
            })
            .AddEndpointFilter<ExceptionHandlerFilter>()
            .WithName("CreateExpense")
            .WithTags("Expenses")
            .Produces(StatusCodes.Status201Created)
            .Produces<ErrorMessages>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}