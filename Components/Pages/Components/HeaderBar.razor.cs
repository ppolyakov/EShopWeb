using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages.Components;

public partial class HeaderBar
{
    [Parameter] 
    public EventCallback<string> OnSearch { get; set; }

    private string SearchTerm { get; set; } = string.Empty;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task PerformSearch()
    {
        await OnSearch.InvokeAsync(SearchTerm);
    }
}