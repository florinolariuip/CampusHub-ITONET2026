namespace CampusHub.Domain.Common;

public sealed class Unit
{
    public static readonly Unit Value = new();
    private Unit() { }
}
