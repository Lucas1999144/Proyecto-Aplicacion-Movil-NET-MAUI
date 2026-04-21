using CommunityToolkit.Mvvm.ComponentModel;
using Parcial_Moviles.Models;

namespace Parcial_Moviles.ViewModels;

[QueryProperty(nameof(Product), "product")]
public partial class DetalleViewModel : BaseViewModel
{   // ViewModel para detalle del producto. 
    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private string title = "Detalle del Producto";

    partial void OnProductChanged(Product? oldValue, Product? newValue)
    {
        if (newValue != null)
            Title = newValue.Title;
    }
}