using Client.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Client.Pages.Create;

public partial class Create
{
    [Inject]
    public IJSRuntime Js { get; set; } = null!;

    public object DummyFormBinder { get; set; } = new();

    private static readonly List<Type> PickerComponents =
    [
        typeof(TextInput),
        typeof(NumberInput),
        typeof(SelectInput),
        typeof(CheckboxInput),
        typeof(DateInput),
        typeof(ButtonComponentChoice),
    ];

    private BaseComponentChoice? CurrentlyEditing { get; set; }
    private List<BaseComponentChoice> AddedComponents { get; set; } = [];

    private bool _isDraggingFromPicker;
    private int? _draggingIndex;
    private Type? _draggingComponentType;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await using var module = await Js.InvokeAsync<IJSObjectReference>("import", CancellationToken, "./Pages/Create/Create.razor.js");
            await module.InvokeAsync<string>("AddEventHandlersToDraggableContainers", CancellationToken);
        }
    }

    private void OnPickerDragStart(Type compType)
    {
        _draggingComponentType = compType;
        _isDraggingFromPicker = true;
    }

    private void OnBuilderDragStart(int index)
    {
        _draggingIndex = index;
        _isDraggingFromPicker = false;
    }

    private void OnDragEnd()
    {
        _draggingIndex = null;
        _draggingComponentType = null;
        _isDraggingFromPicker = false;
    }

    private void OnDropAt(int index)
    {
        if (_isDraggingFromPicker && _draggingComponentType != null)
        {
            var component = BaseComponentChoice.CreateDefault(_draggingComponentType);
            AddedComponents.Insert(index, component);
            CurrentlyEditing = component;
        }
        else if (!_isDraggingFromPicker && _draggingIndex.HasValue)
        {
            var item = AddedComponents[_draggingIndex.Value];
            AddedComponents.RemoveAt(_draggingIndex.Value);

            if (_draggingIndex.Value < index)
            {
                index--;
            }

            AddedComponents.Insert(index, item);
        }

        OnDragEnd();
        StateHasChanged();
    }

    private void RemoveItem(int index)
    {
        // if we remove the component we're currently editing,
        // then stop editing
        if (CurrentlyEditing == AddedComponents[index])
            CurrentlyEditing = null;

        AddedComponents.RemoveAt(index);
        StateHasChanged();
    }

    private void EditItem(int index)
    {
        CurrentlyEditing = AddedComponents[index];
        StateHasChanged();
    }
}