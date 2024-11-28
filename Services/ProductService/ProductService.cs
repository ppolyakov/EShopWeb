using EShopWeb.Data.Models;
using EShopWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace EShopWeb.Services.ProductService;
public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == productId);
    }

    public async Task<bool> AddProductAsync(Product product, byte[] imageData)
    {
        try
        {
            product.ImageData = imageData;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        try
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryID = product.CategoryID;

                if (product.ImageData != null)
                {
                    existingProduct.ImageData = product.ImageData;
                }

                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryID == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }
}