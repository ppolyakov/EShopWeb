namespace EShopWeb.Data.Models;

public class Cart
{
    public int CartID { get; set; }
    public int UserID { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public User User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}