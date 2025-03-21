using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Common;

public class Vec2StringJsonConverter : JsonConverter<Vector2>
{
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetString()!.ParseVector2();

    public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToVector2String());
}