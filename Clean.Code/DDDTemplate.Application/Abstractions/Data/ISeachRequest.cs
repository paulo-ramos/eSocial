namespace DDDTemplate.Application.Abstractions.Data;

public interface ISearchRequest<T>
{
    T[]? RequestedIds { get; set; }
    string Keyword { get; set; }
}