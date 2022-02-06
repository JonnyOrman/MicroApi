namespace MicroApi.Create.Example;

public class ExampleCreator : ICreator<ExampleEntity, ExampleParameters>
{
    private readonly IList<ExampleEntity> _entities;

    public ExampleCreator()
    {
        _entities = new List<ExampleEntity>();
    }

    public async Task<ExampleEntity> CreateAsync(ExampleParameters parameters)
    {
        //This is where you might use the parameters to create a new record in some kind of database
        //and assign the record an ID

        var id = _entities.Any() ? _entities.Last().Key + 1 : 1;

        var entity = new ExampleEntity(
            id,
            parameters.Name,
            parameters.Type
        );

        _entities.Add(entity);

        return entity;
    }
}
