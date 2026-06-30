using CommunityToolkit.Mvvm.ComponentModel;
using Parcial_Moviles.Models;
using CommunityToolkit.Mvvm.Input;

namespace Parcial_Moviles.ViewModels;

[QueryProperty(nameof(Product), "product")]
public partial class DetalleViewModel : BaseViewModel
{
    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private string title = "Detalle del Producto";

    [ObservableProperty]
    private string ubi = string.Empty; /// Muestra ubi en UI

    partial void OnProductChanged(Product? oldValue, Product? newValue)
    {
        if (newValue != null)
        {
            Title = newValue.Title;
            _ = ObtenerUbiAsync();
        }
    }

[RelayCommand]
    private async Task ObtenerUbiAsync()
    {
        try
        {
            var req = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var loc = await Geolocation.GetLocationAsync(req);
            if (loc != null)
            {
                Ubi = $"Latitud: {loc.Latitude:F4}, Longitud: {loc.Longitude:F4}";
            }
            else
                Ubi = "No se encontro la ubicacion";
        }
        catch (FeatureNotSupportedException)
        {
            Ubi = "GPS no disponible para este tipo de dispositivo";
        }
        catch (PermissionException)
        {
            Ubi = "Permiso de ubicacion no otorgado";
        }
        catch (Exception ex)
        {
            Ubi = $"Error: {ex.Message}";
        }
    }
}