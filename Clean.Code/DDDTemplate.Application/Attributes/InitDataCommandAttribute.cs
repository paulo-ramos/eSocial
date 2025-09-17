namespace DDDTemplate.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class InitDataCommandAttribute(int sortIndex = 0) : Attribute
{
    public int SortIndex { get; set; } = sortIndex;
}