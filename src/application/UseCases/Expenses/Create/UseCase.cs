using communication.Enums;
using communication.Requests.Expenses;
using exception.Utils;

namespace application.UseCases.Expenses.Create;

public class UseCase() {
    public void Execute(CreateExpenseRequest request) {
        Validate(request);
    }

    private void Validate(CreateExpenseRequest request) {
        var (title, description, date, amount, paymentType) = request;

        var isTitleValid = !IsNullEmptyOrWhiteSpace.Check(title);
        var isAmountValid = amount >= 0;
        var isDateValid = (DateTime.Compare(date, DateTime.UtcNow)) <= 0;
        var isPaymentTypeValid = Enum.IsDefined<PaymentType>(paymentType);
        
        if (!isTitleValid) throw new ArgumentException("Title is required");
        if (!isAmountValid) throw new ArgumentException("Amount must be greater than 0");
        if (!isDateValid) throw new ArgumentException("Provided date is invalid");
        if(!isPaymentTypeValid) throw new ArgumentException("Payment type is invalid");
    }
}
