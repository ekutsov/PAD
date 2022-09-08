using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.ThemeManager;
using Microsoft.JSInterop;

namespace PAD.Client.Shared;

public partial class MainLayout
{
    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    protected SignOutSessionStateManager SignOutManager { get; set; }

    [Inject]
    protected IJSRuntime _jsRuntime { get; set; }

    private bool _drawerOpen = true;

    private bool _isDarkMode;

    async Task Logout()
    {
        await SignOutManager.SetSignOutState();
        Navigation.NavigateTo("authentication/logout");
    }

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    void ThemeModeToogle()
    {
        _isDarkMode = !_isDarkMode;
    }

    

    private MudTheme _theme = new()
    {
        Palette = new()
        {
            AppbarBackground = Colors.Grey.Lighten1,
            AppbarText = Colors.Grey.Darken4
        }
    };

    private ThemeManagerTheme _themeManager = new ThemeManagerTheme()
    {
        DrawerClipMode = DrawerClipMode.Always,
        FontFamily = "Montserrat",
        Theme = new MudTheme()
        {
            Palette = new Palette()
            {
                AppbarBackground = Colors.Shades.White,
                AppbarText = Colors.Grey.Darken3,
                Background = Colors.Grey.Lighten3
            }
        }
    };

    public bool _themeManagerOpen = false;

    void OpenThemeManager(bool value)
    {
        _themeManagerOpen = value;
    }

    void UpdateTheme(ThemeManagerTheme value)
    {
        _themeManager = value;
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        StateHasChanged();
    }

    public void WriteLog(object obj)
    {
        ((IJSInProcessRuntime)_jsRuntime).Invoke<object>("console.log", obj);
    }
}