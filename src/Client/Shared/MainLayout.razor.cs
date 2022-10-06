namespace PAD.Client.Shared;

public partial class MainLayout
{
    [Inject] protected NavigationManager Navigation { get; set; }

    [Inject] protected SignOutSessionStateManager SignOutManager { get; set; }

    [Inject] protected StateContainer State { get; set; }

    [Inject] protected ILocalStorageService LocalStorage { get; set; }

    private bool _isDrawerOpen = true;

    private bool _isDarkMode = false;


    protected override async Task OnInitializedAsync()
    {
        State.OnChange += StateHasChanged;
        bool? isDrawerOpen = await LocalStorage.GetItemAsync<bool?>("isDrawerOpen");
        if (isDrawerOpen != null)
        {
            _isDrawerOpen = isDrawerOpen.Value;

        }
        else
        {
            await LocalStorage.SetItemAsync<bool>("isDrawerOpen", _isDarkMode);
        }

        bool? isDarkMode = await LocalStorage.GetItemAsync<bool?>("isDarkMode");
        if (isDarkMode != null)
        {
            _isDarkMode = isDarkMode.Value;

        }
        else
        {
            await LocalStorage.SetItemAsync<bool>("isDarkMode", _isDarkMode);
        }
    }

    public void Dispose() => State.OnChange -= StateHasChanged;

    private async Task<bool> DrawerToggle()
    {
        _isDrawerOpen = !_isDrawerOpen;
        await LocalStorage.SetItemAsync<bool>("isDrawerOpen", _isDrawerOpen);
        return _isDarkMode;
    }

    private async Task<bool> ThemeModeToogle()
    {
        _isDarkMode = !_isDarkMode;
        await LocalStorage.SetItemAsync<bool>("isDarkMode", _isDarkMode);
        return _isDarkMode;
    }

    private async Task Logout()
    {
        await SignOutManager.SetSignOutState();
        Navigation.NavigateTo("authentication/logout");
    }

    private ThemeManagerTheme _themeManager = new()
    {
        DrawerClipMode = DrawerClipMode.Always,
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