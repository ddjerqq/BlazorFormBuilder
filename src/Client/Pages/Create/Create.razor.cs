using Microsoft.AspNetCore.Components.Web;

namespace Client.Pages.Create;

public partial class Create
{
    private BaseComponentChoice? CurrentlyEditing { get; set; }

    private static readonly List<Type> PickerComponents =
    [
        typeof(TextInput),
        typeof(NumberInput),
        typeof(SelectInput),
        typeof(CheckboxInput),
        typeof(DateInput),
        typeof(Button),
    ];

    private readonly List<BaseComponentChoice> _builderItems = [];

    private bool _isDraggingFromPicker;
    private int? _draggingIndex;
    private Type? _draggingComponentType;

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

    /// <summary>
    /// even though this method does absolutely nothing, it is required for blazor to fire the appropriate event
    /// </summary>
    private void OnDropZoneDragOver(DragEventArgs e)
    {
    }

    private void OnDropAt(int index)
    {
        if (_isDraggingFromPicker && _draggingComponentType != null)
        {
            var component = BaseComponentChoice.CreateDefault(_draggingComponentType);
            _builderItems.Insert(index, component);
            CurrentlyEditing = component;
        }
        else if (!_isDraggingFromPicker && _draggingIndex.HasValue)
        {
            var item = _builderItems[_draggingIndex.Value];
            _builderItems.RemoveAt(_draggingIndex.Value);

            if (_draggingIndex.Value < index)
            {
                index--;
            }

            _builderItems.Insert(index, item);
        }

        OnDragEnd();
        StateHasChanged();
    }

    private void RemoveItem(int index)
    {
        _builderItems.RemoveAt(index);
        StateHasChanged();
    }

    private void EditItem(int index)
    {
        CurrentlyEditing = _builderItems[index];
        StateHasChanged();
    }
}