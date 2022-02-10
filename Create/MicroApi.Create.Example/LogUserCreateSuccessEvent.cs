namespace MicroApi.Create.Example;

public class LogUserCreateSuccessEvent : IOperationSuccessEvent<User, UserParameters>
{
    public void Run(User entity, UserParameters parameters)
    {
        Console.WriteLine($"{nameof(User)} successfully created with {nameof(entity.Key)} {entity.Key}");
    }
}