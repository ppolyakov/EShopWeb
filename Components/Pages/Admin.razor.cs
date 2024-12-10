using EShopWeb.Data.Models;
using EShopWeb.Services.ProductService;
using EShopWeb.Services.UserService;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class Admin
{
    private List<Product> Products = new();
    private List<Category> Categories = new();
    private List<Product> FilteredProducts = new();
    private List<User> Users = new();
    private List<Role> Roles = new();

    private Product selectedProduct = new();
    private Category selectedCategory = new();
    private User selectedUser = new();

    private string password = string.Empty;

    private bool isEditingProduct = false;
    private bool isEditingCategory = false;
    private bool isEditingUser = false;

    private string notificationMessage = string.Empty;
    private bool isNotificationVisible = false;

    private bool IsAdmin = false;

    [Inject]
    private IProductService ProductService { get; set; } = default!;
    [Inject]
    private IUserService UserService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (_currentUser != null)
        {
            await InitializePageData();
        }
    }

    private User? _currentUser;

    private bool isRendered;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await UserService.GetCurrentUserAsync();
            if (result != null)
            {
                _currentUser = result;
                isRendered = true;
                await InitializePageData();
                StateHasChanged();
            }
        }
    }

    private async Task InitializePageData()
    {
        IsAdmin = _currentUser?.RoleID == (int)RolesEnum.Admin;

        if (!IsAdmin)
        {
            NavigationManager.NavigateTo("/products");
            return;
        }

        Roles = await UserService.GetRolesAsync();
        Categories = await ProductService.GetCategoriesAsync();
        Products = await ProductService.GetAllProductsAsync();
        FilteredProducts = Products;
        Users = await UserService.GetAllUsersAsync();
    }

    private void ClearFormCategory()
    {
        selectedCategory = new Category();
        isEditingCategory = false;
    }

    private async Task SaveCategory(Category category)
    {
        if (!string.IsNullOrWhiteSpace(category.CategoryName))
        {
            if (isEditingCategory)
            {
                await ProductService.UpdateCategoryAsync(category);
                ShowNotification("Category updated successfully.");
            }
            else
            {
                await ProductService.AddCategoryAsync(category);
                ShowNotification("Category added successfully.");
            }

            Categories = await ProductService.GetCategoriesAsync();
            ClearFormCategory();
        }
    }

    private void SelectCategory(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var categoryId))
        {
            selectedCategory = Categories.FirstOrDefault(c => c.CategoryID == categoryId) ?? new Category();
            isEditingCategory = true;
        }
        else
        {
            ClearFormCategory();
        }
    }

    private void ClearFormProduct()
    {
        selectedProduct = new Product();
        isEditingProduct = false;
    }

    private async Task SaveProduct(Product product)
    {
        if (isEditingProduct)
        {
            await ProductService.UpdateProductAsync(product);
            ShowNotification("Product updated successfully.");
        }
        else
        {
            await ProductService.AddProductAsync(product, product.ImageData!);
            ShowNotification("Product added successfully.");
        }

        Products = await ProductService.GetAllProductsAsync();
        FilteredProducts = Products;
        ClearFormProduct();
    }

    private void SelectProduct(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var productId))
        {
            selectedProduct = Products.FirstOrDefault(p => p.ProductID == productId) ?? new Product();
            isEditingProduct = true;
        }
        else
        {
            ClearFormProduct();
        }
    }

    private async Task DeleteCategory(Category category)
    {
        await ProductService.DeleteCategoryAsync(category);
        Categories = await ProductService.GetCategoriesAsync();
        ClearFormCategory();
        ShowNotification("Category deleted successfully.");
    }

    private async Task DeleteProduct(Product product)
    {
        await ProductService.DeleteProductAsync(product);
        Products = await ProductService.GetAllProductsAsync();
        FilteredProducts = Products;
        ClearFormProduct();
        ShowNotification("Product deleted successfully.");
    }

    private void ClearFormUser()
    {
        selectedUser = new User();
        password = string.Empty;
        isEditingUser = false;
    }

    private async Task SaveUser()
    {
        if (!string.IsNullOrWhiteSpace(password))
        {
            selectedUser.PasswordHash = UserService.HashPassword(selectedUser, password);
        }

        if (isEditingUser)
        {
            await UserService.UpdateUserAsync(selectedUser);
            ShowNotification("User updated successfully.");
        }
        else
        {
            await UserService.AddUserAsync(selectedUser);
            ShowNotification("User added successfully.");
        }

        Users = await UserService.GetAllUsersAsync();
        ClearFormUser();
    }

    private async Task DeleteUser(User user)
    {
        await UserService.DeleteUserAsync(user);
        Users = await UserService.GetAllUsersAsync();
        ClearFormUser();
        ShowNotification("User deleted successfully.");
    }

    private void SelectUser(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var userId))
        {
            selectedUser = Users.FirstOrDefault(u => u.UserID == userId) ?? new User();
            isEditingUser = true;
        }
        else
        {
            ClearFormUser();
        }
    }

    private void FilterByCategory(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var categoryId))
        {
            FilteredProducts = Products.Where(p => p.CategoryID == categoryId).ToList();
        }
        else
        {
            FilteredProducts = Products;
        }
    }

    private void ShowNotification(string message)
    {
        notificationMessage = message;
        isNotificationVisible = true;
    }

    private void ClearNotification()
    {
        isNotificationVisible = false;
        notificationMessage = string.Empty;
    }
}