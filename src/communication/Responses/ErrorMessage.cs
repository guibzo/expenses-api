namespace communication.Responses;

public record ErrorMessages(List<string> Errors) {
    public ErrorMessages(string error) : this([error]) {} 
}
