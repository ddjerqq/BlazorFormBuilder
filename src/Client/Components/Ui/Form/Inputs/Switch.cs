using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using TailwindMerge;

namespace Client.Components.Ui.Form.Inputs;

public sealed class Switch : InputCheckbox
{
    public const string BaseClass =
        "peer inline-flex h-5 w-9 shrink-0 cursor-pointer items-center rounded-full border-2 border-transparent " +
        "shadow-sm transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring " +
        "focus-visible:ring-offset-2 focus-visible:ring-offset-background disabled:cursor-not-allowed " +
        "disabled:opacity-50 " +
        "data-[state=checked]:bg-primary data-[state=unchecked]:bg-input " +
        "data-[state=checked]:[&.valid]:bg-green-900 " +
        "data-[state=checked]:[&.invalid]:bg-destructive";

    [Inject]
    public TwMerge Tw { get; set; } = null!;

    [Parameter]
    public string Class { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "button");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "button");
        builder.AddAttribute(3, "role", "switch");
        builder.AddAttribute(4, "data-checked", CurrentValue);
        builder.AddAttribute(5, "data-state", CurrentValue ? "checked" : "unchecked");
        builder.AddAttributeIfNotNullOrWhiteSpace(6, "name", NameAttributeValue);
        builder.AddAttribute(7, "class", Tw.Merge(BaseClass, CssClass, Class));
        builder.AddAttribute(8, "onclick", Toggle);
        builder.AddElementReferenceCapture(9, __inputReference => Element = __inputReference);

        builder.AddContent(10, contentBuilder =>
        {
            contentBuilder.OpenElement(1, "span");
            contentBuilder.AddAttribute(2, "data-state", CurrentValue ? "checked" : "unchecked");
            contentBuilder.AddAttribute(3, "class",
                "pointer-events-none block size-4 rounded-full bg-background shadow-lg ring-0 " +
                "transition-transform data-[state=checked]:translate-x-4 data-[state=unchecked]:translate-x-0");
            builder.AddAttribute(8, "onclick", Toggle);
            contentBuilder.CloseElement();
        });

        builder.OpenElement(11, "input");
        builder.AddAttribute(12, "hidden");
        builder.AddAttribute(13, "value", CurrentValue);
        builder.AddAttribute(14, "onchange", ValueChanged);
        builder.CloseElement();

        builder.CloseElement();
    }

    private async Task Toggle()
    {
        CurrentValue = !CurrentValue;
        await ValueChanged.InvokeAsync(Value);
    }
}