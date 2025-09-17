namespace NaviSmart.Application.Data;

public class TreeSelectList<TResult>
    where TResult: class
{
    public static TResult[] Create<TEntity>(List<TEntity> dataSource, TEntity parent,
        Func<TEntity, TResult[], TResult> mapping, Func<TEntity, TEntity, bool> filter, Func<TEntity, object>? sort = null)
        where TEntity : class 
    {
        var filteredData = dataSource.Where(x => filter(x, parent));
        if (sort is not null)
            filteredData = filteredData.OrderBy(sort);

        return (from item in filteredData.ToList()
            select mapping(item, Create(dataSource, item, mapping, filter, sort))).ToArray();
    }

    public static List<TResult> RemoveParentNoChildren(List<TResult> dataSource,
        Func<TResult, bool> getLeafChildrenFilter, 
        Func<TResult, List<TResult>, bool> getParentFilter)
    {
        var temp = dataSource.Where(getLeafChildrenFilter).ToList();
        while (true)
        {
            var parents = dataSource.Where(x => getParentFilter(x, temp)).ToList();
            if (!parents.Any())
                break;
            
            temp.AddRange(parents);
        }

        return temp;
    }

    public static TResult[] RemoveParentNoChildren(TResult[] dataSource,
        Func<TResult, bool> getLeafChildrenFilter,
        Func<TResult, List<TResult>, bool> getParentFilter) =>
        RemoveParentNoChildren(dataSource.ToList(), getLeafChildrenFilter, getParentFilter).ToArray();
}