using application.UseCases.Expenses.Create;
using communication.Requests.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Expenses.Create;

public static class CreateExpenseEndpoint  {
    public static void MapCreateExpense(this IEndpointRouteBuilder app)  {
        app.MapPost("/expenses", ([FromBody] CreateExpenseRequest request) => {
            UseCase useCase = new();
            
            try {
                useCase.Execute(request);
            }
            catch (Exception exc)  {
                return exc switch {
                    ArgumentException => Results.BadRequest(exc.Message),
                    _ => Results.InternalServerError("Unknown error (500) at CreateExpense.")
                };
            }
            
            return Results.Created();
        })
        .WithName("GetExpenses")
        .WithTags("Expenses")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}