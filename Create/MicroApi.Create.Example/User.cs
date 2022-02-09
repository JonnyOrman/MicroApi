namespace MicroApi.Create.Example;

public class User : Entity<int>
{
    public User(
        int key,
        string name,
        string email
        ) : base(key)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; }
    public string Email { get; }
}