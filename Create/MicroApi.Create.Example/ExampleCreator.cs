using MicroApi.Core;
using MicroApi.Create;

namespace MicroApi.Create.Example;

public class ExampleCreator : ICreator<ExampleEntity, ExampleParameters>
{
    private readonly IList<ExampleEntity> _entities;

    public ExampleCreator()
    {
        _entities = new List<ExampleEntity>();
    }

    public async Task<ExampleEntity> Create(ExampleParameters parameters)
    {
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
