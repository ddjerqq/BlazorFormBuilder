using System.Security.Cryptography;

namespace Client.Common;

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
            nameof(TextInput) => new TextInput
            {
                InputType = "text",
                Placeholder = "...",
                Label = "TextInput",
            },
            nameof(NumberInput) => new NumberInput
            {
                Label = "NumberInput",
            },
            nameof(SelectInput) => new SelectInput
            {
                Choices = [],
                Label = "SelectInput",
            },
            nameof(CheckboxInput) => new CheckboxInput
            {
                Label = "CheckboxInput",
            },
            nameof(DateInput) => new DateInput
            {
                InputType = "datetime-local",
                Label = "DateInput",
            },
            nameof(ButtonComponentChoice) => new ButtonComponentChoice
            {
                ButtonText = "Hire me!",
                Label = "Button",
            },
            _ => throw new ArgumentException("Invalid component type"),
        };
    }
}