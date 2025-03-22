using System.Numerics;

namespace Domain.Common;

public static class Vector2Ext
{
    public static Vector2 ParseVector2(this string value)
    {
        if (value?.Split(':') is [var x, var y]
            && float.TryParse(x, out var parsedX)
            && float.TryParse(y, out var parsedY))
            return new Vector2(parsedX, parsedY);
        
        throw new FormatException("Invalid Vector2 format");
    }
    
    public static string ToVector2String(this Vector2 vector) => $"{vector.X}:{vector.Y}";
}