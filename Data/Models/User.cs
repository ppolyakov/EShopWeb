using System.Data;
using System.Text.Json.Serialization;

namespace EShopWeb.Data.Models;

public class User
{
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
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