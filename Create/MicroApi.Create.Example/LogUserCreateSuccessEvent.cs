namespace MicroApi.Create.Example;

public class LogUserCreateSuccessEvent : IOperationSuccessEvent<User, UserParameters>
{
    private readonly IUserCreateMessageGenerator _userCreateMessageGenerator;

    public LogUserCreateSuccessEvent(IUserCreateMessageGenerator userCreateMessageGenerator)
    {
        _userCreateMessageGenerator = userCreateMessageGenerator;
    }

    public void Run(User user, UserParameters parameters)
    {
        var message = _userCreateMessageGenerator.Generate(user);

        Console.WriteLine(message);
    }
}