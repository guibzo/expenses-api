namespace exception.Exceptions;

public class CustomValidationException : AppException {
    public List<string> ErrorMessages { get; }

    public CustomValidationException(List<string> errorMessages) {
        ErrorMessages = errorMessages;
    } 
    
    public CustomValidationException(string errorMessage) : this([errorMessage]) {}
}
