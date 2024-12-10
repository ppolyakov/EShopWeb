using System.ComponentModel.DataAnnotations;

namespace EShopWeb.Data.Models;

public class Review
{
    public int ReviewID { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
    public int Rating { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Comment message must not be empty.")]
    public string Comment { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }

    public Product Product { get; set; }
    public User User { get; set; }
}