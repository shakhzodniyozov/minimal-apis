using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi;

public class MinimalApisDocumentFilter : IDocumentFilter
{
    private readonly IEnumerable<EndpointDataSource> _endpointSources;

    public MinimalApisDocumentFilter(IEnumerable<EndpointDataSource> endpointSources)
    {
        _endpointSources = endpointSources;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var endpoints = _endpointSources.SelectMany(es => es.Endpoints);
        
        foreach (var endpoint in endpoints)
        {
            var routeEndpoint = endpoint as RouteEndpoint;
            if (routeEndpoint == null) continue;
            var httpMethods = endpoint.Metadata
                .OfType<HttpMethodMetadata>()
                .FirstOrDefault()?.HttpMethods;

            if (httpMethods == null || httpMethods.Count == 0)
                continue;

            foreach (var method in httpMethods)
            {
                var path = "/" + routeEndpoint.RoutePattern.RawText.TrimStart('/');

                var apiVersionMetadata = endpoint.Metadata.OfType<ApiVersionMetadata>().FirstOrDefault();
                var apiVersion = apiVersionMetadata?.Map(ApiVersionMapping.Explicit).SupportedApiVersions.FirstOrDefault();
                path = path.Replace("{version:apiVersion}", apiVersion is null ? string.Empty : apiVersion.ToString());

                if (!swaggerDoc.Paths.ContainsKey(path))
                {
                    swaggerDoc.Paths[path] = new OpenApiPathItem();
                }

                swaggerDoc.Paths[path].Operations[ToOperationType(method)] = new OpenApiOperation
                {
                    Summary = "Auto-generated from Minimal API",
                    Parameters = ExtractParameters(routeEndpoint),
                    Responses = new OpenApiResponses
                    {
                        ["200"] = new OpenApiResponse { Description = "Success" }
                    }
                };
            }
        }
    }

    private static OperationType ToOperationType(string method) => method.ToLower() switch
    {
        "get" => OperationType.Get,
        "post" => OperationType.Post,
        "put" => OperationType.Put,
        "delete" => OperationType.Delete,
        "patch" => OperationType.Patch,
        _ => OperationType.Get
    };
    
    private static List<OpenApiParameter> ExtractParameters(RouteEndpoint routeEndpoint)
    {
        var parameters = new List<OpenApiParameter>();

        foreach (var param in routeEndpoint.RoutePattern.Parameters)
        {
            if (param.Name == "version") continue;
            
            parameters.Add(new OpenApiParameter
            {
                Name = param.Name,
                In = ParameterLocation.Path,
                Required = true,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }

        return parameters;
    }
}