# MicroApi
`MicroApi` allows you to easily create minimal web APIs in .NET with specific CRUD operations.
This is useful when you want to separate create, read, update and delete responsibilities between separate services.
`MicroApi` allows you to build a working minimal API very quickly, while also offering extensibility such as adding custom validation and result handling.

The solution includes the following packages:
- [MicroApi](https://www.nuget.org/packages/MicroApi/)
- [MicroApi.Create](https://www.nuget.org/packages/MicroApi.Create/)
- [MicroApi.Read](https://www.nuget.org/packages/MicroApi.Read/)
- [MicroApi.Update](https://www.nuget.org/packages/MicroApi.Update/)
- [MicroApi.Delete](https://www.nuget.org/packages/MicroApi.Delete/)

## [MicroApi](https://www.nuget.org/packages/MicroApi/)

The `MicroApi` package is the base package containing common functionality and utilities shared between each micro API. This includes things like the base `Entity` model, validation and result handling.

## [MicroApi.Create](https://www.nuget.org/packages/MicroApi.Create/)

The `MicroApi.Create` package allows you to create a minimal API for data creation.

See the `MicroApi.Create.Example` project in the solution for an example on how to use it.

### Getting started

Create a new .NET project:
```
dotnet new console --name UserCreateApi
```
Change to the project's directory:
```
cd UserCreateApi
```
Add the `MicroApi.Create` package:
```
dotnet add package MicroApi.Create --version 1.0.0-alpha.2
```
In the project directory create a `User.cs` file and add the following entity class to it:
```
using MicroApi;

namespace UserCreateApi;

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
```

Create a `UserParameters.cs` file and add the following parameters class to it:
```
namespace UserCreateApi;

public class UserParameters
{
    public string Name { get; set; }
    public string Email { get; set; }
}
```

Create a `UserCreator.cs` file and add the following creator class to it:
```
using MicroApi.Create;

namespace UserCreateApi;

public class UserCreator : ICreator<User, UserParameters>
{
    private readonly ICollection<User> _users;

    public UserCreator()
    {
        _users = new List<User>();
    }

    public async Task<User> CreateAsync(UserParameters userParameters)
    {
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
```
In this example we are just storing our entities in a collection, but this is where you would probably want to create the entity by writing the parameters to some kind of database and assigning a key.

Finally, replace the contents of `Program.cs` with the following:
```
using MicroApi.Create;
using UserCreateApi;

MicroCreateApi.New<User, int, UserParameters, UserCreator>(args).Start();
```

Run the project:
```
dotnet run
```

Submit a `POST` to `localhost:5000/`, setting the `Content-Type` header to `application/json` and providing the following body:
```
{
    "name": "John Doe",
    "email": "john.doe@gmail.com"
}
```
You will receive a 201 response with the following body:
```
{
    "entity": {
        "name": "John Doe",
        "email": "john.doe@gmail.com",
        "key": 1
    },
    "isSuccessful": true,
    "message": "Success!"
}
```

### Validation

Make the `Name` property in the parameters required by modifying the contents of `Program.cs` like so:
```
using MicroApi.Create;
using UserCreateApi;

MicroCreateApi.New<User, int, UserParameters, UserCreator>(args)
    .Where(parameters => parameters.Name).IsRequired()
    .Start();
```

Run the project again and try submitting a body where a name is not provided:
```
{
    "name": null,
    "email": "john.doe@gmail.com"
}
```

You will receive a 400 response with a body detailing the problems with the parameters:
```
{
    "invalidParameters": [
        {
            "parameterName": "Name",
            "messages": [
                "Value is null"
            ]
        }
    ],
    "isSuccessful": false,
    "message": "Invalid parameters provided"
}
```

Let's add validation to the `Email` property. Create a new file called `EmailRule.cs` and add the following class to it:
```
using MicroApi;

namespace UserCreateApi;

public class EmailRule : IValidationRule<UserParameters>
{
    public ValidationRuleResult Validate(UserParameters value)
    {
        //keep it simple for demo purposes
        var isValid = value.Email.Contains('@') && value.Email.Contains('.');

        if (isValid)
        {
            return new ValidationRuleResult(true);
        }
        
        return new InvalidPropertyRuleResult(nameof(value.Email), $"{value.Email} is not a valid email");
    }
}
```

Update `Program.cs` to use the email rule:
```
using MicroApi.Create;
using UserCreateApi;

MicroCreateApi.New<User, int, UserParameters, UserCreator>(args)
    .Where(parameters => parameters.Name).IsRequired()
    .MustPass<EmailRule>()
    .Start();
```

Run the project again and try submitting a body where the email does not meet the requirements in the rule:
```
{
    "name": "John Doe",
    "email": "johndoegmailcom"
}
```

You will receive another 400 response:
```
{
    "invalidParameters": [
        {
            "parameterName": "Email",
            "messages": [
                "johndoegmailcom is not a valid email"
            ]
        }
    ],
    "isSuccessful": false,
    "message": "Invalid parameters provided"
}
```

## [MicroApi.Read](https://www.nuget.org/packages/MicroApi.Read/)

In progress.

## [MicroApi.Update](https://www.nuget.org/packages/MicroApi.Update/)

In progress.

## [MicroApi.Delete](https://www.nuget.org/packages/MicroApi.Delete/)

In progress.