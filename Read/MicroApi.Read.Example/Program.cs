using MicroApi.Read;
using MicroApi.Read.Example;

MicroReadApi.New<ExampleEntity, int, ExampleQuery, ExampleSingleReader, ExampleCollectionReader>(args,
        serviceCollection =>
        {
            serviceCollection.AddSingleton<IEnumerable<ExampleEntity>, Database>();
        })
    .Where(exampleQuery => exampleQuery.Type)
    .MustPass<TypeRule>()
    .OnGetSingleSuccess<SingleLogger>()
    .OnGetCollectionSuccess<CollectionLogger>()
    .Start();