namespace exception.Exceptions;

public abstract class AppException : SystemException {
    public abstract List<string> GetErrors();
    public abstract int StatusCode { get; }
}
