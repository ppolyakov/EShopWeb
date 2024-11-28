using EShopWeb.Data.Models;

namespace EShopWeb.Services.ProductService;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<bool> AddProductAsync(Product product, byte[] imageData);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int productId);
    Task<List<Category>> GetCategoriesAsync();
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
}