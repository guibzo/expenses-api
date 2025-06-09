using System.Net;

namespace exception.Exceptions;

public class CustomValidationException(List<string> errorMessages) : AppException { 
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    
    public override List<string> GetErrors() {
        return errorMessages;
    }
}
