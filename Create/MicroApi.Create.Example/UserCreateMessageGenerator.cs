namespace MicroApi.Create.Example;

public class UserCreateMessageGenerator : IUserCreateMessageGenerator
{
    public string Generate(User user)
    {
        return $"{nameof(User)} successfully created with {nameof(user.Key)} {user.Key}";
    }
}
