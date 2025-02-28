using Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Components;

public static class BaseComponentChoiceExt
{
    public static InputDateType GetInputDateType(this DateInput dateInput) => dateInput.InputType switch
    {
        "date" => InputDateType.Date,
        "month" => InputDateType.Month,
        "time" => InputDateType.Time,
        "datetime-local" => InputDateType.DateTimeLocal,
        _ => throw new ArgumentOutOfRangeException(nameof(DateInput.InputType), "Invalid InputType"),
    };
}