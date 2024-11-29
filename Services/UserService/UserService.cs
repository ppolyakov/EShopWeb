using EShopWeb.Data;
using EShopWeb.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShopWeb.Services.UserService;
public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly PasswordHasher<User> _passwordHasher;
    private User? _currentUser;
    private const string CurrentUserKey = "CurrentUser";

    public UserService(AppDbContext context, ProtectedSessionStorage sessionStorage)
    {
        _context = context;
        _sessionStorage = sessionStorage;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == userId);
    }

    public async Task<bool> RegisterUserAsync(User user)
    {
        var roleExists = await _context.Roles.AnyAsync(r => r.RoleID == user.RoleID);
        if (!roleExists)
        {
            throw new InvalidOperationException("Invalid RoleID. Role does not exist.");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> AuthenticateUserAsync(string username, string password)
    {
        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        if (user != null)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }
        }
        return null;
    }

    public async Task AddUserAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            throw new ArgumentException("Password cannot be empty.");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == user.UserID);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        existingUser.Username = user.Username;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.Phone = user.Phone;
        existingUser.Address = user.Address;
        existingUser.RoleID = user.RoleID;

        if (!string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            existingUser.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == user.UserID);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var result = await _sessionStorage.GetAsync<User>(CurrentUserKey);
        return result.Success ? result.Value : null;
    }

    public async Task SetCurrentUserAsync(User user)
    {
        await _sessionStorage.SetAsync(CurrentUserKey, user);
    }

    public async Task ClearCurrentUserAsync()
    {
        await _sessionStorage.DeleteAsync(CurrentUserKey);
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }
}