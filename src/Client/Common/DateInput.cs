using Microsoft.AspNetCore.Components.Forms;

namespace Client.Common;

/// <summary>
/// Only allowed values for InputType are: date, month, week, time, datetime-local
/// </summary>
public sealed class DateInput : BaseComponentChoice
{
    public DateTime Value { get; set; } = DateTime.UtcNow;
    
    public required string InputType { get; set; }

    public InputDateType InputDateType => InputType switch
    {
        "date" => InputDateType.Date,
        "month" => InputDateType.Month,
        "time" => InputDateType.Time,
        "datetime-local" => InputDateType.DateTimeLocal,
        _ => throw new ArgumentOutOfRangeException(nameof(InputType), "Invalid InputType"),
    };
}