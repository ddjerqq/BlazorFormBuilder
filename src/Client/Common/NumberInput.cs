namespace Client.Common;

public sealed class NumberInput : BaseComponentChoice
{
    public float Value { get; set; } = 0;
    public float Min { get; set; } = 0;
    public float Max { get; set; } = 1;
    public float Step { get; set; } = 0.1f;
}