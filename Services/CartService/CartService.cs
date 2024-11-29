using EShopWeb.Data.Models;
using System.Collections.Concurrent;

namespace EShopWeb.Services.CartService;
public class CartService : ICartService
{
    private readonly ConcurrentDictionary<int, CartItem> _cartItems = new();

    public Task AddToCartAsync(Product product, int quantity)
    {
        if (_cartItems.ContainsKey(product.ProductID))
        {
            _cartItems[product.ProductID].Quantity += quantity;
        }
        else
        {
            _cartItems[product.ProductID] = new CartItem
            {
                Product = product,
                Quantity = quantity
            };
        }

        return Task.CompletedTask;
    }

    public Task RemoveFromCartAsync(int productId)
    {
        _cartItems.TryRemove(productId, out _);
        return Task.CompletedTask;
    }

    public Task<List<CartItem>> GetCartItemsAsync()
    {
        return Task.FromResult(_cartItems.Values.ToList());
    }

    public Task ClearCartAsync()
    {
        _cartItems.Clear();
        return Task.CompletedTask;
    }
}