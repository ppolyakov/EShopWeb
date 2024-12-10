using EShopWeb.Data.Models;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages.Components;

public partial class ProductList
{
    [Parameter]
    public List<Product>? Products { get; set; }
    [Parameter]
    public bool CanDeleteProducts { get; set; }
    [Parameter]
    public EventCallback<Product> OnDeleteRequested { get; set; }
    [Parameter]
    public EventCallback<int> OnViewProduct { get; set; }

    private async Task ConfirmDelete(Product product)
    {
        await OnDeleteRequested.InvokeAsync(product);
    }

    private async Task ViewProductDetails(int productId)
    {
        await OnViewProduct.InvokeAsync(productId);
    }
}