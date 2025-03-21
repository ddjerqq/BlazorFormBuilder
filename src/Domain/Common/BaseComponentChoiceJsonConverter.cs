using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.Common;

public class BaseComponentChoiceConverter : JsonConverter<BaseComponentChoice>
{
    public override BaseComponentChoice Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        if (!jsonDoc.RootElement.TryGetProperty("type", out var typeProperty))
        {
            throw new JsonException("Missing type discriminator.");
        }

        var typeDiscriminator = typeProperty.GetString() ?? throw new JsonException("Invalid type discriminator.");

        var json = jsonDoc.RootElement.GetRawText();

        return typeDiscriminator switch
        {
            "text" => JsonSerializer.Deserialize<TextInput>(json, options) ?? throw new JsonException("Failed to deserialize TextInput."),
            "number" => JsonSerializer.Deserialize<NumberInput>(json, options) ?? throw new JsonException("Failed to deserialize NumberInput."),
            "select" => JsonSerializer.Deserialize<SelectInput>(json, options) ?? throw new JsonException("Failed to deserialize SelectInput."),
            "checkbox" => JsonSerializer.Deserialize<CheckboxInput>(json, options) ?? throw new JsonException("Failed to deserialize CheckboxInput."),
            "date" => JsonSerializer.Deserialize<DateInput>(json, options) ?? throw new JsonException("Failed to deserialize DateInput."),
            "button" => JsonSerializer.Deserialize<ButtonComponentChoice>(json, options) ?? throw new JsonException("Failed to deserialize ButtonComponentChoice."),
            "grid" => JsonSerializer.Deserialize<GridComponent>(json, options) ?? throw new JsonException("Failed to deserialize GridComponent."),
            _ => throw new JsonException($"Unknown type discriminator: {typeDiscriminator}")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseComponentChoice value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        switch (value)
        {
            case TextInput text:
                writer.WriteString("type", "text");
                WriteProperties(writer, text, options);
                break;
            case NumberInput number:
                writer.WriteString("type", "number");
                WriteProperties(writer, number, options);
                break;
            case SelectInput select:
                writer.WriteString("type", "select");
                WriteProperties(writer, select, options);
                break;
            case CheckboxInput checkbox:
                writer.WriteString("type", "checkbox");
                WriteProperties(writer, checkbox, options);
                break;
            case DateInput date:
                writer.WriteString("type", "date");
                WriteProperties(writer, date, options);
                break;
            case ButtonComponentChoice button:
                writer.WriteString("type", "button");
                WriteProperties(writer, button, options);
                break;
            case GridComponent grid:
                writer.WriteString("type", "grid");
                WriteProperties(writer, grid, options);
                break;
            default:
                throw new NotSupportedException($"Type {value.GetType()} is not supported by this converter.");
        }

        writer.WriteEndObject();
    }

    private static void WriteProperties<T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var element = JsonSerializer.SerializeToElement(value, options);
        foreach (var property in element.EnumerateObject().Where(property => !property.NameEquals("type")))
        {
            property.WriteTo(writer);
        }
    }
}