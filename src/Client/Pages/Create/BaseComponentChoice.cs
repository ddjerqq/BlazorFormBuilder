using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages.Create;

public abstract class BaseComponentChoice
{
    public string Id { get; set; } = RandomNumberGenerator.GetHexString(5);
    public required string Label { get; set; }
    public string? Description { get; set; }
    public bool Required { get; set; } = false;

    public static BaseComponentChoice CreateDefault(Type componentType)
    {
        return componentType.Name switch
        {
            "TextInput" => new TextInput
            {
                InputType = "text",
                Placeholder = "...",
                Label = "TextInput",
            },
            "NumberInput" => new NumberInput
            {
                Label = "NumberInput",
            },
            "SelectInput" => new SelectInput
            {
                Choices = [],
                Label = "SelectInput",
            },
            "CheckboxInput" => new CheckboxInput
            {
                Label = "CheckboxInput",
            },
            "DateInput" => new DateInput
            {
                InputType = "datetime-local",
                Label = "DateInput",
            },
            "Button" => new Button
            {
                ButtonText = "Hire me!",
                Label = "Button",
            },
            _ => throw new ArgumentException("Invalid component type"),
        };
    }
}

public sealed class TextInput : BaseComponentChoice
{
    private string _inputType = null!;

    public required string InputType
    {
        get => _inputType;
        set
        {
            if (value is not ("text" or "tel" or "url" or "password" or "search"))
                throw new ArgumentException("InputType must be one of the following: text, tel, url, password, search");

            _inputType = value;
        }
    }
    public required string Placeholder { get; set; }
    public string DefaultValue { get; set; } = string.Empty;
}

public sealed class NumberInput : BaseComponentChoice
{
    public float Min { get; set; } = 0;
    public float Max { get; set; } = 1;
    public float Step { get; set; } = 0.1f;
    public float DefaultValue { get; set; } = 0;
}

public sealed class SelectInput : BaseComponentChoice
{
    public required List<string> Choices { get; set; }
}

public sealed class CheckboxInput : BaseComponentChoice;

/// <summary>
/// Only allowed values for InputType are: date, month, week, time, datetime-local
/// </summary>
public sealed class DateInput : BaseComponentChoice
{
    private string _inputType = null!;

    public required string InputType
    {
        get => _inputType;
        set
        {
            // Blazor's InputDate component doesn't support weeks. so we exclude it
            if (value is not ("date" or "month" or "time" or "datetime-local"))
                throw new ArgumentException("InputType must be one of the following: date, month, time, datetime-local");

            _inputType = value;
        }
    }

    public InputDateType InputDateType => InputType switch
    {
        "date" => InputDateType.Date,
        "month" => InputDateType.Month,
        "time" => InputDateType.Time,
        "datetime-local" => InputDateType.DateTimeLocal,
        _ => throw new ArgumentOutOfRangeException(nameof(InputType), "Invalid InputType"),
    };

    public DateTime DefaultValue { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// The type="submit" button for the form.
/// We could also have buttons of type="button" but that is outside the scope of this project
/// </summary>
public sealed class Button : BaseComponentChoice
{
    public required string ButtonText { get; set; }
}