namespace Domain.Common;

public static class StringExt
{
    public static string? FromEnv(this string key) =>
        Environment.GetEnvironmentVariable(key);

    public static string FromEnv(this string key, string value) =>
        Environment.GetEnvironmentVariable(key) ?? value;

    public static string FromEnvRequired(this string key) =>
        Environment.GetEnvironmentVariable(key)
        ?? throw new InvalidOperationException($"Environment variable not found: {key}");

    public static string ToSnakeCase(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(text), "Text was null");

        if (text.Length < 2)
            return text;

        var sb = new System.Text.StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));

        for (var i = 1; i < text.Length; ++i)
        {
            var c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}