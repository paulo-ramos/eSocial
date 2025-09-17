namespace DDDTemplate.Application.Abstractions.Data;

public interface IPageListRequest
{
    string? SortBy { get; set; }
    string? SortDir { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
}