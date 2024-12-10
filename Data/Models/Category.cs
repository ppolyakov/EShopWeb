using System.ComponentModel.DataAnnotations;

namespace EShopWeb.Data.Models;

public class Category
{
    public int CategoryID { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Category name must not be empty.")]
    public string CategoryName { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}