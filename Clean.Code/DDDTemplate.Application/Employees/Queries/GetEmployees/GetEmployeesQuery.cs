using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Data;

namespace DDDTemplate.Application.Employees.Queries.GetEmployees;

public class GetEmployeesQuery : ISearchRequest<Guid>, IPageListRequest, IQuery<PageList<GetEmployeesQueryResult>>
{
    public Guid[]? RequestedIds { get; set; }
    public string Keyword { get; set; } = string.Empty;
    public string? SortBy { get; set; }
    public string? SortDir { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}