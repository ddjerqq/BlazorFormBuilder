using Client.Services;
using Domain.Aggregates;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages.Edit;

public partial class Edit
{
    [Inject]
    public IJSRuntime Js { get; set; } = null!;

    [Inject]
    public FormApiClient Api { get; set; } = null!;

    [Parameter]
    public Guid Id { get; set; }

    private bool _hasChangesSinceLastSave;

    private UserForm? UserForm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserForm = await Api.GetById(Id, CancellationToken);
        if (UserForm is not null)
        {
            UserForm.SortFields();
        }
        else
        {
            ShowError("Form with the given ID could not be found!");
        }
    }

    private async Task SaveForm()
    {
        _hasChangesSinceLastSave = false;
        UserForm!.SortFields();
        await Api.UpdateForm(UserForm!, CancellationToken);
        ShowSuccess("Form saved successfully");
    }

    private void Preview()
    {
        NavigationManager.NavigateTo($"/preview/{Id}");
    }

    #region Editing logic

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

    private void OnDropAt(int index)
    {
        _hasChangesSinceLastSave = true;
        if (_isDraggingFromPicker && _draggingComponentType != null)
        {
            var component = BaseComponentChoice.CreateDefault(_draggingComponentType, UserForm!.Id);
            UserForm.Fields.Insert(index, component);
            UserForm.SyncFieldOrders();
            CurrentlyEditing = component;
        }
        else if (!_isDraggingFromPicker && _draggingIndex.HasValue)
        {
            var item = UserForm!.Fields[_draggingIndex.Value];
            UserForm.Fields.RemoveAt(_draggingIndex.Value);

            if (_draggingIndex.Value < index)
            {
                index--;
            }

            UserForm.Fields.Insert(index, item);
            UserForm.SyncFieldOrders();
        }

        OnDragEnd();
        StateHasChanged();
    }

    private void RemoveItem(int index)
    {
        _hasChangesSinceLastSave = true;
        // if we remove the component we're currently editing,
        // then stop editing
        if (CurrentlyEditing == UserForm!.Fields[index])
            CurrentlyEditing = null;

        UserForm.Fields.RemoveAt(index);
        UserForm.SyncFieldOrders();
        StateHasChanged();
    }

    private void EditItem(int index)
    {
        _hasChangesSinceLastSave = true;
        CurrentlyEditing = UserForm!.Fields[index];
        StateHasChanged();
    }

    #endregion
}