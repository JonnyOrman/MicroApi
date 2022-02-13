using MicroApi.Create;
using MicroApi.Create.Example;
using Microsoft.Extensions.DependencyInjection;

MicroCreateApi.New<User, int, UserParameters, UserCreator>(args, serviceCollection =>
    {
        serviceCollection.AddSingleton<IUserCreateMessageGenerator, UserCreateMessageGenerator>();
    })
    .Where(parameters => parameters.Name).IsRequired()
    .MustPass<EmailRule>()
    .OnSuccess<LogUserCreateSuccessEvent>()
    .Start();
