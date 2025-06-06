using communication.Enums;

namespace communication.Requests.Expenses;
public record CreateExpenseRequest (string Title, string? Description, DateTime Date, decimal Amount, string PaymentType);