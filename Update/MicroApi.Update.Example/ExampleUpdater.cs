namespace MicroApi.Update.Example;

public class ExampleUpdater : IOperation<ExampleEntity, ExampleParameters>
{
    private readonly IList<ExampleEntity> _entities;

    public ExampleUpdater()
    {
        _entities = new List<ExampleEntity>
        {
            new ExampleEntity(1, "entity1", "a"),
            new ExampleEntity(2, "entity2", "b"),
            new ExampleEntity(3, "entity3", "a")
        };
    }

    public async Task<ExampleEntity> ExecuteAsync(ExampleParameters parameters)
    {
        var entity = _entities.FirstOrDefault(entity => entity.Key == parameters.Key);

        if (entity != null)
        {
            var newEntity = new ExampleEntity(
                entity.Key,
                parameters.Name ?? entity.Name,
                parameters.Type ?? parameters.Type
            );

            _entities[_entities.IndexOf(entity)] = newEntity;

            entity = newEntity;
        }

        return entity;
    }
}
