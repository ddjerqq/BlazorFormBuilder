namespace Domain.Entities;

/// <summary>
/// Only allowed values for InputType are: date, month, week, time, datetime-local
/// </summary>
public sealed class DateInput : BaseComponentChoice
{
    public DateTime SelectedDate { get; set; } = DateTime.UtcNow;

    public required string InputType { get; set; }
}