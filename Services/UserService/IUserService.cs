using EShopWeb.Data.Models;

namespace EShopWeb.Services.UserService;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> AuthenticateUserAsync(string username, string password);
    Task<bool> RegisterUserAsync(User user);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<User?> GetCurrentUserAsync();
    Task SetCurrentUserAsync(User user);
    Task<List<Role>> GetRolesAsync();
    string HashPassword(User user, string password);
}