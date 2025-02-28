namespace Client.Common;

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
}