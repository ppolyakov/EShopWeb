namespace EShopWeb.Data.Models;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryID { get; set; }
    public byte[]? ImageData { get; set; }

    public Category Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Review> Reviews { get; set; }
}