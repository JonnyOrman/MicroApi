namespace MicroApi.Delete.Example;

public class ExampleDeleter : IOperation<ExampleEntity, int>
{
    private readonly List<ExampleEntity> _entities;

    public ExampleDeleter()
    {
        _entities = new List<ExampleEntity>
        {
            new ExampleEntity(1),
            new ExampleEntity(2),
            new ExampleEntity(3)
        };
    }

    public async Task<ExampleEntity> ExecuteAsync(int key)
    {
        var entity = _entities.FirstOrDefault(exampleEntity => exampleEntity.Key == key);

        if (entity != null)
        {
            _entities.Remove(entity);
        }

        return entity;
    }
}
