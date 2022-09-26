namespace PAD.Client.Shared;

public partial class MainLayout
{
    [Inject] protected NavigationManager Navigation { get; set; }

    [Inject] protected SignOutSessionStateManager SignOutManager { get; set; }

    [Inject] protected StateContainer State { get; set; }

    private bool _drawerOpen = true;

    private bool _isDarkMode = true;

    private void DrawerToggle() => _drawerOpen = !_drawerOpen;

    private void ThemeModeToogle() => _isDarkMode = !_isDarkMode;

    private async Task Logout()
    {
        await SignOutManager.SetSignOutState();
        Navigation.NavigateTo("authentication/logout");
    }

    private ThemeManagerTheme _themeManager = new()
    {
        DrawerClipMode = DrawerClipMode.Docked,
        FontFamily = "Montserrat",
        Theme = new MudTheme()
        {
            Palette = new()
            {
                AppbarBackground = Colors.Shades.White,
                AppbarText = Colors.Grey.Darken3,
                Background = Colors.Grey.Lighten3
            }
        }
    };
}