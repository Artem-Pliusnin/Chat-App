namespace ChatApp.Data.Shared;

public class OperationResult
{
    public static OperationResult Success => new OperationResult() { Succeeded = true };
    public static OperationResult Failure => new OperationResult() { Failed = true };

    protected OperationResult() { }
    public bool Succeeded { get; private set; }
    public bool Failed { get; private set; }

    public string Message {  get; set; }
}