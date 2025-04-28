using MinimalApi.Library.Endpoints;

namespace Application.Common.Endpoint;

public abstract class BaseEndpoint<TResponse> : EndpointBase<TResponse>, IClientEndpoint where TResponse : class
{
}

public abstract class BaseEndpoint<TRequest, TResponse> : EndpointBase<TRequest, TResponse>, IClientEndpoint
    where TResponse : class where TRequest : class
{
    
}