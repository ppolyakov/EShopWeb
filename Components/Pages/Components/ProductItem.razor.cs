using EShopWeb.Data.Models;
using EShopWeb.Services.CartService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages.Components;

public partial class ProductItem
{
    [Parameter]
    public Product Product { get; set; } = new Product();

    [Inject]
    private ICartService CartService { get; set; } = default!;

    private async Task AddToCart()
    {
        if (Product is null) return;

        await CartService.AddToCartAsync(Product, 1);
    }
}