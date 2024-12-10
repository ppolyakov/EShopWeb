using EShopWeb.Data.Models;
using EShopWeb.Services.CartService;
using EShopWeb.Services.ProductService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class ProductDetails
{
    private Product? product;
    private string notificationMessage = string.Empty;
    private bool isNotificationVisible = false;
    private int quantity = 1;

    [Parameter] 
    public int productId { get; set; }

    [SupplyParameterFromQuery]
    [Parameter] public int categoryId { get; set; }

    [Inject] 
    private IProductService ProductService { get; set; } = default!;
    [Inject]
    private ICartService CartService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        product = await ProductService.GetProductByIdAsync(productId);

        if (product == null)
        {
            NavigationManager.NavigateTo("/products");
        }
    }

    private void NavigateBack()
    {
        NavigationManager.NavigateTo($"/products?categoryId={categoryId}");
    }

    private async Task AddToCart()
    {
        if (product != null && quantity > 0)
        {
            await CartService.AddToCartAsync(product, quantity);
            notificationMessage = $"{quantity} x {product.Name} has been added to your cart.";
            isNotificationVisible = true;
        }
    }

    private void ClearNotification()
    {
        isNotificationVisible = false;
        notificationMessage = string.Empty;
    }
}