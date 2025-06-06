using communication.Requests.Expenses;
using exception.Exceptions;
using FluentValidation;

namespace application.UseCases.Expenses.Create;

public sealed class CreateExpenseUseCase() {
    public void Execute(CreateExpenseRequest request) {
        var paramsValidator = new CreateExpenseParamsValidator();
        var validationResult = paramsValidator.Validate(request);

        if (!validationResult.IsValid) {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new CustomValidationException(errorMessages);
        };        
    }
}
