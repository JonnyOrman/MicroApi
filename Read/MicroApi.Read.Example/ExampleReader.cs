namespace MicroApi.Read.Example;

public class ExampleReader : IReader<ExampleEntity, int, ExampleQuery>
{
    private readonly IList<ExampleEntity> _entities;

    public ExampleReader()
    {
        _entities = new List<ExampleEntity>
        {
            new ExampleEntity(1),
            new ExampleEntity(2),
            new ExampleEntity(3)
        };
    }

    public async Task<ExampleEntity> ReadAsync(int key)
    {
        return _entities.FirstOrDefault(e => e.Key == key);
    }

    public async Task<IEnumerable<ExampleEntity>> ReadManyAsync(ExampleQuery query)
    {
        return new List<ExampleEntity>();
    }
}