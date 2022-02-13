namespace MicroApi.Read.Example;

public class Database : List<ExampleEntity>
{
    public Database()
    {
        Add(new ExampleEntity(1, "entity1", "a"));
        Add(new ExampleEntity(2, "entity2", "b"));
        Add(new ExampleEntity(3, "entity3", "a"));
    }
}
