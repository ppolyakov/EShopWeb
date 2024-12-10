using EShopWeb.Data.Models;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages.Components;

public partial class CategoryForm
{
    [Parameter] 
    public Category Category { get; set; } = new Category();
    [Parameter] 
    public bool IsEditing { get; set; }
    [Parameter] 
    public EventCallback<Category> OnSave { get; set; }
    [Parameter] 
    public EventCallback<Category> OnClear { get; set; }
    [Parameter] 
    public EventCallback<Category> OnDelete { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task HandleSubmit()
    {
        await OnSave.InvokeAsync(Category);
    }

    private async Task Clear()
    {
        await OnClear.InvokeAsync(Category);
    }

    private async Task Delete(Category category)
    {
        await OnDelete.InvokeAsync(category);
    }
}