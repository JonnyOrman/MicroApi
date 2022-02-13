using MicroApi.Create;
using UserCreateApi;

MicroCreateApi.New<User, int, UserParameters, UserCreator>(args)
    .Where(parameters => parameters.Name).IsRequired()
    .Start();
