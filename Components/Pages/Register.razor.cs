using EShopWeb.Data.Models;
using EShopWeb.Services.UserService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class Register
{
    private User user = new User();
    private string password = string.Empty;

    [Inject] 
    private IUserService UserService { get; set; } = default!;
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task HandleRegister()
    {
        try
        {
            user.PasswordHash = password;
            user.RoleID = (int)RolesEnum.Customer;
            var result = await UserService.RegisterUserAsync(user);
            if (result)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}