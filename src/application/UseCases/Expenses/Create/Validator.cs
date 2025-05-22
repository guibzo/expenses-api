using communication.Requests.Expenses;
using FluentValidation;

namespace application.UseCases.Expenses.Create;

public class CreateExpenseParamsValidator : AbstractValidator<CreateExpenseRequest> {
    public CreateExpenseParamsValidator() {
        RuleFor(expense => expense.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        RuleFor(expense => expense.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A future date is invalid");
        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage("Payment type invalid");
    }
}