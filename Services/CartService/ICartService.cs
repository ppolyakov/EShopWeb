using EShopWeb.Data.Models;

namespace EShopWeb.Services.CartService;

public interface ICartService
{
    Task AddToCartAsync(Product product, int quantity);
    Task RemoveFromCartAsync(int productId);
    Task<List<CartItem>> GetCartItemsAsync();
    Task ClearCartAsync();
}