using EShopWeb.Data.Models;
using EShopWeb.Services.UserService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class Login
{
    private User user = new User();
    private string username = string.Empty;
    private string password = string.Empty;

    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject] 
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task HandleLogin()
    {
        var user = await UserService.AuthenticateUserAsync(username, password);
        if (user != null)
        {
            await UserService.SetCurrentUserAsync(user);
            NavigationManager.NavigateTo("/products");
        }
    }

    private async Task LoginAsGuest()
    {
        await UserService.SetCurrentUserAsync(new User { RoleID = (int)RolesEnum.Customer, Username = "Guest" });
        NavigationManager.NavigateTo("/products");
    }
}