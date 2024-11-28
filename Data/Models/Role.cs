namespace EShopWeb.Data.Models;

public class Role
{
    public int RoleID { get; set; }
    public Roles RoleName { get; set; }

    public ICollection<User> Users { get; set; }
}

public enum Roles
{
    Admin,
    Customer
}