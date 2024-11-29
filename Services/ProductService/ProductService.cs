﻿using EShopWeb.Data.Models;
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

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == productId);
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryID == categoryId)
            .ToListAsync();
    }

    public async Task<bool> AddProductAsync(Product product, byte[] imageData)
    {
        product.ImageData = imageData;
        _context.Products.Add(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        var productExisting = await _context.Products.FindAsync(product.ProductID);
        if (productExisting != null)
        {
            _context.Products.Remove(productExisting);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<bool> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        var categoryExisting = await _context.Categories.FindAsync(category.CategoryID);
        if (categoryExisting != null)
        {
            _context.Categories.Remove(categoryExisting);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}