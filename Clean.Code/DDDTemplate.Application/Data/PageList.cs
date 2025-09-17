using System.Linq.Expressions;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Domain.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Application.Data;

public class PageList<T>
{
    public List<T> Items { get; set; }
    public int Total { get; set; }

    public PageList()
    {
    }

    private PageList(List<T> items, int total)
    {
        Items = items;
        Total = total;
    }

    public static async Task<PageList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        return pageSize == 0
            ? new PageList<T>(await query.ToListAsync(), totalCount)
            : new PageList<T>(await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(), totalCount);
    }

    public static async Task<PageList<T>> CreateAsync<TEntity>(IQueryable<TEntity> query,
        Expression<Func<TEntity, T>> mapping, IPageListRequest pageListRequest,
        Expression<Func<TEntity, object>>? sort = null)
        where TEntity : class
    {
        var totalCount = await query.CountAsync();

        if (sort is not null)
            query = "descend".Equals(pageListRequest.SortDir, StringComparison.OrdinalIgnoreCase)
                ? query.OrderByDescending(sort)
                : query.OrderBy(sort);

        if (pageListRequest.PageSize != 0)
            query = query.Skip((pageListRequest.Page - 1) * pageListRequest.PageSize).Take(pageListRequest.PageSize);

        return new PageList<T>(await query.Select(mapping).ToListAsync(), totalCount);
    }
    
    public static PageList<T> Create() => new([], 0);

    public static PageList<T> Create(List<T> list) => new(list, list.Count);
}