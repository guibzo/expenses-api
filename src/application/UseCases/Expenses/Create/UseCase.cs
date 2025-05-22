using communication.Requests.Expenses;
using FluentValidation;

namespace application.UseCases.Expenses.Create;

public sealed class CreateExpenseUseCase() {
    public void Execute(CreateExpenseRequest request) {
        var paramsValidator = new CreateExpenseParamsValidator();
        var validationResult = paramsValidator.Validate(request);
        
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
    }

    
}
