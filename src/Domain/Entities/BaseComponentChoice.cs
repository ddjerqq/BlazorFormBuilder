using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

[JsonConverter(typeof(BaseComponentChoiceConverter))]
public abstract class BaseComponentChoice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Label { get; set; }
    public string? Description { get; set; }
    public bool Required { get; set; } = false;

    public int Order { get; set; }

    public required Guid FormId { get; set; }

    public static BaseComponentChoice CreateDefault(Type componentType, Guid formId) => componentType.Name switch
    {
        nameof(TextInput) => new TextInput
        {
            FormId = formId,
            InputType = "text",
            Placeholder = "...",
            Label = "TextInput",
        },
        nameof(NumberInput) => new NumberInput
        {
            FormId = formId,
            Label = "NumberInput",
        },
        nameof(SelectInput) => new SelectInput
        {
            FormId = formId,
            Choices = [],
            Label = "SelectInput",
        },
        nameof(CheckboxInput) => new CheckboxInput
        {
            FormId = formId,
            Label = "CheckboxInput",
        },
        nameof(DateInput) => new DateInput
        {
            FormId = formId,
            InputType = "datetime-local",
            Label = "DateInput",
        },
        nameof(ButtonComponentChoice) => new ButtonComponentChoice
        {
            FormId = formId,
            ButtonType = "button",
            ButtonText = "Hire me!",
            Label = "Button",
        },
        _ => throw new ArgumentException("Invalid component type"),
    };
}