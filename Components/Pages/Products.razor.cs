using BlazorBootstrap;
using EShopWeb.Data.Models;
using EShopWeb.Services.ProductService;
using EShopWeb.Services.UserService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class Products
{
    private Modal modal = default!;
    private List<Product>? products;
    private List<Category>? categories;
    private int selectedCategoryId;
    private Product? productToDelete;

    [Inject] 
    private IProductService ProductService { get; set; } = default!;
    [Inject] 
    private IUserService UserService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

        categories = await ProductService.GetCategoriesAsync();

        if (query["categoryId"] != null && int.TryParse(query["categoryId"], out var categoryId))
        {
            selectedCategoryId = categoryId;
        }
        else
        {
            selectedCategoryId = 0;
        }

        await LoadProductsByCategory(selectedCategoryId);
    }


    private async Task LoadProductsByCategory(int categoryId)
    {
        selectedCategoryId = categoryId;

        if (categoryId == 0)
        {
            products = await ProductService.GetAllProductsAsync();
        }
        else
        {
            products = await ProductService.GetProductsByCategoryAsync(categoryId);
        }
    }

    private void NavigateToDetails(int productId)
    {
        NavigationManager.NavigateTo($"/product-details/{productId}?categoryId={selectedCategoryId}");
    }

    private async void ShowDeleteConfirmation(Product product)
    {
        var user = await UserService.GetCurrentUserAsync();
        if (user?.RoleID != (int)RolesEnum.Admin)
        {
            return;
        }

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

    private async void CancelDelete()
    {
        productToDelete = null;
        await modal.HideAsync();
    }
}