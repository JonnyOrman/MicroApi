using MicroApi.Create;
using MicroApi.Create.Example;

MicroCreateApi.New<ExampleEntity, int, ExampleParameters, ExampleCreator>(args)
    .Where(exampleParameters => exampleParameters.Name).IsRequired()
    .Start();