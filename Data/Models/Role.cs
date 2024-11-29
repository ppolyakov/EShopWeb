using System.Text.Json.Serialization;

namespace EShopWeb.Data.Models;

public class Role
{
    public int RoleID { get; set; }
    public RolesEnum RoleName { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; }
}

public enum RolesEnum
{
    Admin = 1,
    Customer = 2
}