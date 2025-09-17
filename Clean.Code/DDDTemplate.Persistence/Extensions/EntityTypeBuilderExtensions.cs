using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void HasEnumData<TEntity, TEnums, TAttribute>(this EntityTypeBuilder<TEntity> builder,
        Func<object, string, TAttribute, TEntity> factory)
        where TEntity : class
        where TEnums : Enum
        where TAttribute : Attribute
    {
        var enumType = typeof(TEnums);
        var enumNames = enumType.GetEnumNames();
        if (enumNames.Length == 0)
            return;

        var rows = (from enumName in enumNames
            let field = enumType.GetField(enumName)
            let enumAttr = field?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault()
            where enumAttr is not null
            let enumValue = Enum.Parse(enumType, enumName)
            select factory(enumValue, enumName, (TAttribute)enumAttr)
            into row
            where row is not null
            select row).ToList();

        if (rows.Count == 0)
            return;

        builder.HasData(rows);
    }
}