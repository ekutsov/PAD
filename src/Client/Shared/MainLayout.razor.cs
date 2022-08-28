using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System.Threading.Tasks;
using System;

namespace PAD.Client.Shared;

public partial class MainLayout
{
    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    protected SignOutSessionStateManager SignOutManager { get; set; }

    [Inject]
    protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private string UserName { get; set; }

    protected bool isSidebarExpanded = true;

    protected override async Task OnInitializedAsync()
    {
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        UserName = authState.User.Identity.Name;
    }

    void SidebarToggleClick()
    {
        isSidebarExpanded = !isSidebarExpanded;
    }

    protected async Task ProfileMenuClick(RadzenProfileMenuItem args)
    {
        switch (args.Text)
        {
            case "Logout":
                await SignOutManager.SetSignOutState();
                Navigation.NavigateTo("authentication/logout");
                break;
            default:
                throw new NotImplementedException();
        }
    }
}