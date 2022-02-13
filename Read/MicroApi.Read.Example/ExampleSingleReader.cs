namespace MicroApi.Read.Example;

public class ExampleSingleReader : IOperation<ExampleEntity, int>
{
    private readonly IEnumerable<ExampleEntity> _database;

    public ExampleSingleReader(IEnumerable<ExampleEntity> database)
    {
        _database = database;
    }

    public async Task<ExampleEntity> ExecuteAsync(int key)
    {
        //This is where you might look up a record from some kind of database by the provided key
        
        return _database.FirstOrDefault(exampleEntity => exampleEntity.Key == key);
    }
}