using System.Reflection;

namespace MicroApi.Read.Example;

public class ExampleQuery
{
    public static ValueTask<ExampleQuery?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        return ValueTask.FromResult<ExampleQuery?>(
            new ExampleQuery()
        );
    }
}