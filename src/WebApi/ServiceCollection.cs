using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace WebApi;

public static class ServiceCollection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.Configure<JsonOptions>(opts =>
            opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
        
        services.AddHttpContextAccessor();
        
        return services;
    }
}