using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

[JsonConverter(typeof(BaseComponentChoiceConverter))]
public abstract class BaseComponentChoice
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Label { get; set; }
    public string? Description { get; set; }
    public bool Required { get; set; }

    public int Order { get; set; }

    public required Guid FormId { get; init; }

    public static BaseComponentChoice CreateDefault(Type componentType, Guid formId) => componentType.Name switch
    {
        nameof(TextInput) => new TextInput
        {
            FormId = formId,
            InputType = "text",
            Label = "Text",
        },
        nameof(NumberInput) => new NumberInput
        {
            FormId = formId,
            Label = "Number",
        },
        nameof(SelectInput) => new SelectInput
        {
            FormId = formId,
            Choices = [],
            Label = "Select",
        },

        nameof(CheckboxInput) => new CheckboxInput
        {
            FormId = formId,
            Label = "Switch",
        },
        nameof(DateInput) => new DateInput
        {
            FormId = formId,
            InputType = "datetime-local",
            Label = "Date",
        },
        nameof(ButtonComponentChoice) => new ButtonComponentChoice
        {
            FormId = formId,
            ButtonType = "button",
            ButtonText = "Submit",
            Label = "Button",
        },

        nameof(GridComponent) => new GridComponent()
        {
            FormId = formId,
            Label = "Grid",
        },
        _ => throw new ArgumentException("Invalid component type"),
    };
}