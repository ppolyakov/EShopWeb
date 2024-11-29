using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShopWeb.Data.Models;

public class User
{
    public int UserID { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    [RegularExpression(@"\S.*", ErrorMessage = "Username cannot be empty or whitespace.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[A-Za-z])[A-Za-z\d!@#$%^&*]{8,}$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int RoleID { get; set; }

    [JsonIgnore]
    public Role Role { get; set; }
    [JsonIgnore]
    public ICollection<Order> Orders { get; set; }
    [JsonIgnore]
    public ICollection<Review> Reviews { get; set; }
    [JsonIgnore]
    public Cart Cart { get; set; }
}