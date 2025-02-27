using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;

// ReSharper disable once CheckNamespace, this is necessary here
namespace Microsoft.AspNetCore.Components;

public static class RenderTreeBuilderExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddAttributeIfNotNullOrWhiteSpace(this RenderTreeBuilder builder, int sequence, string name, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            builder.AddAttribute(sequence, name, value);
    }
}