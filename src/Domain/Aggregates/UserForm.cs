using Domain.Entities;
using Newtonsoft.Json;

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

    //Relationships with User + nav property
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; } = null!;

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