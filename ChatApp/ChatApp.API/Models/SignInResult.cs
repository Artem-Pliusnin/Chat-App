namespace ChatApp.API.Models;

public class SignInResult
{
    public static SignInResult Success => new SignInResult() { Succeeded = true };
    public static SignInResult Failure =>  new SignInResult() { Failed = true };
    public static SignInResult Error(string error) => new SignInResult() { Failed = true, ErrorMessage = error };
    
    private SignInResult() { }
    
    public bool Succeeded { get; private set; }
    public bool Failed { get; private set; }
    public string ErrorMessage { get; private set; }
    
}