using System.Diagnostics.CodeAnalysis;
using Client.Components.Shared;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Ui;

/// <summary>
/// A base for all ui component.
/// This component makes it easier to merge tailwindcss classes
/// </summary>
public abstract class UiComponentBase : AppComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = [];

    protected string? Class => AdditionalAttributes.GetValueOrDefault("class") as string;

    /// <summary>
    /// Merges tailwind classes, automatically applying the component prop class
    /// </summary>
    protected string? Merge([StringSyntax("html")] params string?[] classes) => Tw.Merge(classes.Append(Class).ToArray());
}