using MicroApi.Create;
using MicroApi.Create.Example;

MicroCreateApi.New<User, int, UserParameters, UserCreator>(args)
    .Where(parameters => parameters.Name).IsRequired()
    .MustPass<EmailRule>()
    .OnSuccess<LogUserCreateSuccessEvent>()
    .Start();