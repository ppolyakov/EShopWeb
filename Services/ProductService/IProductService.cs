using EShopWeb.Data.Models;

namespace EShopWeb.Services.ProductService;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<bool> AddProductAsync(Product product, byte[] imageData);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);

    Task<List<Category>> GetCategoriesAsync();
    Task<bool> AddCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Category category);
}