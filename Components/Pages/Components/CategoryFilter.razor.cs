using EShopWeb.Data.Models;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages.Components;

public partial class CategoryFilter
{
    [Parameter]
    public List<Category>? Categories { get; set; }
    [Parameter]
    public int SelectedCategoryId { get; set; }
    [Parameter]
    public EventCallback<int> OnCategoryChanged { get; set; }

    private async Task ChangeCategory(int categoryId)
    {
        await OnCategoryChanged.InvokeAsync(categoryId);
    }
}