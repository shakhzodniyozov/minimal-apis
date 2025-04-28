using System.Reflection;
using Application.Common.Endpoint;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Library.Endpoints;

namespace Application;

public static class EndpointCollection
{
    public static void AddClientEndpoints(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        AddEndpointsFromAssembly(Assembly.GetExecutingAssembly(), services);
    }
    
    public static void UseClientEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(1, 0)
            .Build();

        var mapGroup = app.MapGroup("api/client/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .HasApiVersion(1, 0)
            .WithTags("Client");

        var endpointServices = app.ServiceProvider.GetServices<IClientEndpoint>();

        foreach (var endpointService in endpointServices)
        {
            endpointService.AddRoute(mapGroup);
        }
    }
    
    private static void AddEndpointsFromAssembly(Assembly assembly, IServiceCollection services)
    {
        var clientEndpoints = assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && x.BaseType != null &&
                        x.GetInterfaces().Contains(typeof(IClientEndpoint)))
            .ToList();
        
        var clientEndpointHandlers = assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && x.BaseType != null &&
                        x.GetInterfaces().Contains(typeof(IEndpointHandlerBase)))
            .ToList();
            
        foreach (var handler in clientEndpointHandlers)
        {
            services.AddScoped(handler);
        }

        foreach (var clientEndpoint in clientEndpoints)
        {
            services.AddSingleton(typeof(IClientEndpoint), clientEndpoint);
        }
    }
}