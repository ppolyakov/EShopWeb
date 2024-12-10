using BlazorBootstrap;
using EShopWeb.Data.Models;
using EShopWeb.Services.ProductService;
using EShopWeb.Services.UserService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace EShopWeb.Components.Pages;

public partial class Index
{
    private List<Product>? products;
    private List<Category>? categories;
    private int selectedCategoryId;
    private Product? productToDelete;
    private bool canDeleteProducts;
    private Modal modal = default!;
    private string searchTerm = string.Empty;

    [Inject]
    private IProductService ProductService { get; set; } = default!;
    [Inject]
    private IUserService UserService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        canDeleteProducts = user.Identity.IsAuthenticated && user.IsInRole("Admin");

        categories = await ProductService.GetCategoriesAsync();
        selectedCategoryId = 0;
        await LoadProductsByCategory(selectedCategoryId);
    }

    private async Task LoadProductsByCategory(int categoryId)
    {
        selectedCategoryId = categoryId;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            if (categoryId == 0)
            {
                products = await ProductService.GetAllProductsAsync();
            }
            else
            {
                products = await ProductService.GetProductsByCategoryAsync(categoryId);
            }
        }
        else
        {
            products = await ProductService.SearchProductsAsync(searchTerm, categoryId);
        }
    }

    private void NavigateToDetails(int productId)
    {
        NavigationManager.NavigateTo($"/product-details/{productId}?categoryId={selectedCategoryId}");
    }

    private async Task ShowDeleteConfirmation(Product product)
    {
        if (!canDeleteProducts) return;

        productToDelete = product;
        await modal.ShowAsync();
    }

    private async Task ConfirmDelete()
    {
        if (productToDelete == null) return;

        var result = await ProductService.DeleteProductAsync(productToDelete);
        if (result)
        {
            await LoadProductsByCategory(selectedCategoryId);
        }
        await modal.HideAsync();
    }

    private async Task CancelDelete()
    {
        productToDelete = null;
        await modal.HideAsync();
    }

    private async Task HandleSearch(string term)
    {
        searchTerm = term;
        await LoadProductsByCategory(selectedCategoryId);
    }
}