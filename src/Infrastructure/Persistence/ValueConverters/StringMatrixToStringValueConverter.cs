using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.ValueConverters;

public class StringMatrixToStringValueConverter() :
    ValueConverter<string[][], string>(
        to => StringToDataConverter(to),
        from => DataToStringConverter(from))
{
    private static string[][] DataToStringConverter(string from) => JsonSerializer.Deserialize<string[][]>(from)!;
    private static string StringToDataConverter(string[][] to) => JsonSerializer.Serialize(to);
}