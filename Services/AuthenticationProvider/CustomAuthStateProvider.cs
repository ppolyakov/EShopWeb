using EShopWeb.Data.Models;
using EShopWeb.Services.UserService;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EShopWeb.Services.AuthenticationProvider;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IUserService _userService;

    public CustomAuthStateProvider(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await _userService.GetCurrentUserAsync();
        ClaimsIdentity identity;

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            };

            if (user.RoleID == (int)RolesEnum.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            identity = new ClaimsIdentity(claims, "CustomAuth");
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    public void NotifyAuthenticationStateChangedTask()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}