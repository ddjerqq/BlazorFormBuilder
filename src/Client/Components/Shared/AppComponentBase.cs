using Blazored.Toast.Configuration;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using TailwindMerge;

namespace Client.Components.Shared;

/// <summary>
/// One base component for all child components.
/// This includes useful utilities that may be needed in any component,
/// most notably the CancellationToken and toasting utilities.
/// </summary>
public abstract class AppComponentBase : ComponentBase, IDisposable
{
    private CancellationTokenSource? _cancellationTokenSource;

    [Inject]
    protected TwMerge Tw { get; set; } = null!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    protected IToastService Toast { get; set; } = null!;

    protected CancellationToken CancellationToken => (_cancellationTokenSource ??= new CancellationTokenSource()).Token;

    protected void ShowSuccess(string message, Action<ToastSettings>? settings = null) => Toast.ShowSuccess(message, settings);
    protected void ShowInfo(string message, Action<ToastSettings>? settings = null) => Toast.ShowInfo(message, settings);
    protected void ShowWarning(string message, Action<ToastSettings>? settings = null) => Toast.ShowWarning(message, settings);
    protected void ShowError(string message, Action<ToastSettings>? settings = null) => Toast.ShowError(message, settings);

    /// <inheritdoc />
    void IDisposable.Dispose()
    {
        GC.SuppressFinalize(this);

        if (_cancellationTokenSource is null)
            return;

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = null;
    }
}