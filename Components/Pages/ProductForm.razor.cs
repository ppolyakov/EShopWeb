using EShopWeb.Data.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace EShopWeb.Components.Pages;

public partial class ProductForm
{
    private byte[]? imageData;

    [Parameter] 
    public Product Product { get; set; } = new Product();
    [Parameter] 
    public EventCallback<Product> OnSave { get; set; }
    [Parameter] 
    public EventCallback<Product> OnClear { get; set; }
    [Parameter] 
    public EventCallback<Product> OnDelete { get; set; }
    [Parameter] 
    public List<Category> Categories { get; set; } = new();
    [Parameter] 
    public bool IsEditing { get; set; }

    private async Task HandleFileUpload(InputFileChangeEventArgs e)
    {
        var file = e.File;

        if (file != null)
        {
            using var stream = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);
            imageData = stream.ToArray();
        }
    }

    private async Task HandleSubmit()
    {
        if (imageData != null)
        {
            Product.ImageData = imageData;
        }

        await OnSave.InvokeAsync(Product);
    }

    private async Task Clear()
    {
        await OnClear.InvokeAsync(Product);
    }

    private async Task Delete(Product product)
    {
        await OnDelete.InvokeAsync(product);
    }
}