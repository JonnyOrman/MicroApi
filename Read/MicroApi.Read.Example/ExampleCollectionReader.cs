namespace MicroApi.Read.Example;

public class ExampleCollectionReader : IOperation<IEnumerable<ExampleEntity>, ExampleQuery>
{
    private readonly IEnumerable<ExampleEntity> _database;

    public ExampleCollectionReader(IEnumerable<ExampleEntity> database)
    {
        _database = database;
    }

    public async Task<IEnumerable<ExampleEntity>> ExecuteAsync(ExampleQuery query)
    {
        //This is where you might get a collection of records from some kind of database
        //filtered by the provided query
        
        return _database.Where(exampleEntity => exampleEntity.Type == query.Type);
    }
}
