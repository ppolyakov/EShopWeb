using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShopWeb.Data.Models;

public class User
{
    public int UserID { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Username must not be empty.")]
    public string Username { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password must not be empty.")]
    [Length(6, 50)]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name must not be empty.")]
    public string FirstName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name must not be empty.")]
    public string LastName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email address must not be empty.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email.")]
    public string Email { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number must not be empty.")]
    [Phone(ErrorMessage = "Please provide a valid phone number.")]
    public string Phone { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Address must not be empty.")]
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