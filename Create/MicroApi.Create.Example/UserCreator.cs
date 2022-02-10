namespace MicroApi.Create.Example;

public class UserCreator : IOperation<User, UserParameters>
{
    private readonly ICollection<User> _users;

    public UserCreator()
    {
        _users = new List<User>();
    }

    public async Task<User> ExecuteAsync(UserParameters userParameters)
    {
        //This is where you might use the parameters to create a new record in some kind of database
        //and assign the record an ID
        
        var id = _users.Any() ? _users.Last().Key + 1 : 1;

        var entity = new User(
            id,
            userParameters.Name,
            userParameters.Email
        );

        _users.Add(entity);

        return entity;
    }
}