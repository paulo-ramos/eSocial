using FastEndpoints;

namespace DDDTemplate.FastEndpoint.Endpoints;

public class EndpointWithoutRequestBase<TResponse> : EndpointBase<EmptyRequest, TResponse>;