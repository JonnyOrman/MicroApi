namespace MicroApi.Read.Example;

public class ExampleReader : IReader<ExampleEntity, int, ExampleQuery>
{
    private readonly IList<ExampleEntity> _entities;

    public ExampleReader()
    {
        _entities = new List<ExampleEntity>
        {
            new ExampleEntity(1, "entity1", "a"),
            new ExampleEntity(2, "entity2", "b"),
            new ExampleEntity(3, "entity3", "a")
        };
    }

    public async Task<ExampleEntity> ReadAsync(int key)
    {
        //This is where you might look up a record from some kind of database by the provided key

        return _entities.FirstOrDefault(e => e.Key == key);
    }

    public async Task<IEnumerable<ExampleEntity>> ReadManyAsync(ExampleQuery query)
    {
        //This is where you might get a collection of records from some kind of database
        //filtered by the provided query

        return _entities.Where(entity => entity.Type == query.Type);
    }
}