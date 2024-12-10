using EShopWeb.Data.Models;
using EShopWeb.Services.CartService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class Cart
{
    private List<CartItem>? cartItems;
    private decimal totalPrice = 0;

    [Inject]
    private ICartService CartService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        cartItems = await CartService.GetCartItemsAsync();
        UpdateTotalPrice();
    }

    private async Task RemoveFromCart(int productId)
    {
        await CartService.RemoveFromCartAsync(productId);
        cartItems = await CartService.GetCartItemsAsync();
        UpdateTotalPrice();
    }

    private async Task ClearCart()
    {
        await CartService.ClearCartAsync();
        cartItems = await CartService.GetCartItemsAsync();
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        totalPrice = cartItems?.Sum(item => item.Product.Price * item.Quantity) ?? 0;
    }

    private void UpdateQuantity(int newQuantity, CartItem item)
    {
        if (newQuantity > 0)
        {
            item.Quantity = newQuantity;
            UpdateTotalPrice();
        }
    }

    private void ChangeQuantity(CartItem item, int change)
    {
        var newQuantity = item.Quantity + change;
        if (newQuantity > 0)
        {
            item.Quantity = newQuantity;
            UpdateTotalPrice();
        }
    }
}