using PFA.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace PFA.Client.Pages;

[Authorize]
public partial class Logout
{
    // [Inject]
    // protected IAccountService AccountService { get; set; }
    [Inject]
    protected NavigationManager Navigation { get; set; }

    protected override async void OnInitialized()
    {
        // await AccountService.LogoutAsync();
        Navigation.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(Navigation.Uri)}");
    }
}