using Domain.Entities;

namespace Domain.Aggregates;

/// <summary>
/// This class is responsible for saving, loading, serializing the forms that the user creates.
/// </summary>
public sealed class UserForm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;

    public List<BaseComponentChoice> Fields { get; set; } = [];

    public void SortFields()
    {
        Fields = Fields.OrderBy(x => x.Order).ToList();
    }

    public void SyncFieldOrders()
    {
        foreach (var (idx, field) in Fields.Index())
        {
            field.Order = idx;
        }

        SortFields();
    }
}