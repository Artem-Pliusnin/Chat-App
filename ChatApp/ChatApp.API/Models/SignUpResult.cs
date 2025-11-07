namespace ChatApp.API.Models;

public class SignUpResult
{
    public static SignUpResult Success => new SignUpResult() { Succeeded = true };
    public static SignUpResult Failure => new SignUpResult() { Failed = true };
    public static SignUpResult EmailFailure => new SignUpResult() { IsEmailAlreadyExists = true };
    public static SignUpResult UserNameFailure => new SignUpResult() { IsUserNameAlreadyExists = true };
    public static SignUpResult Error(string error) => new SignUpResult() { Failed = true, ErrorMessage = error };
    
    private SignUpResult() { }
    
    public bool Succeeded { get; private set; }
    public bool Failed { get; private set; }
    
    public bool IsEmailAlreadyExists { get; private set; }
    
    public bool IsUserNameAlreadyExists { get; private set; }
    public string ErrorMessage { get; private set; }
}