using System.Reflection;

namespace MicroApi.Read.Example;

public class ExampleQuery
{
    public ExampleQuery(string type)
    {
        Type = type;
    }

    public string Type { get; }

    public static ValueTask<ExampleQuery?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        return ValueTask.FromResult<ExampleQuery?>(
            new ExampleQuery(httpContext.Request.Query["type"])
        );
    }
}