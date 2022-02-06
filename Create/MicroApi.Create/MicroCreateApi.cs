using Microsoft.AspNetCore.Builder;

namespace MicroApi.Create;
public static class MicroCreateApi
{
    public static void Start<T, TKey, TParameters, TCreator>(string[] args)
        where T : Entity<TKey>
        where TCreator : class, ICreator<T, TParameters>
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCreate<T, TKey, TParameters, TCreator>();
        
        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapPost("/", async (TParameters parameters, IPostRequestHandler<TParameters> handler) =>
        {
            return await handler.HandleAsync(parameters);
        });
        
        app.Run();
    }
}