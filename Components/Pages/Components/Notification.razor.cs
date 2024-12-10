using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages.Components;

public partial class Notification
{
    [Parameter] 
    public string Message { get; set; } = string.Empty;
    [Parameter] 
    public bool IsVisible { get; set; }
    [Parameter] 
    public EventCallback OnClose { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (IsVisible)
        {
            await Task.Delay(3000);
            IsVisible = false;
            await OnClose.InvokeAsync();
        }
    }
}