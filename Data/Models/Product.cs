using System.ComponentModel.DataAnnotations;

namespace EShopWeb.Data.Models;

public class Product
{
    public int ProductID { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product name must not be empty.")]
    public string Name { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Product description must not be empty.")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Price must be provided.")]
    [Range(0.01, 999999, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public byte[]? ImageData { get; set; }

    public int CategoryID { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Review> Reviews { get; set; }
}