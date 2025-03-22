using System.Numerics;

namespace Domain.Entities;

public sealed class GridComponent : BaseComponentChoice
{
    public Vector2 Dimensions { get; set; } = new(0, 0);

    public string[][] Data { get; set; } =
    [
        ["Invoice", "Status"],
        ["INV001", "Paid"]
    ];
}