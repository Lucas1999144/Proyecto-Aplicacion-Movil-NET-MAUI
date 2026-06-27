using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Parcial_Moviles.Models;
using Parcial_Moviles.Services;
using System.Collections.ObjectModel;

namespace Parcial_Moviles.ViewModels;
public partial class MainViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly DatabaseService _db;
    private CancellationTokenSource? _cts;

    public ObservableCollection<Product> Products { get; } = new();

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string statusMessage = string.Empty;

    public MainViewModel(IApiService api, DatabaseService db)
    {
        _api = api;
        _db = db;
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        ErrorMessage = string.Empty;
        StatusMessage = "Se estan cargando los productos...";
        Products.Clear();

        _cts = new CancellationTokenSource();
        IsBusy = true;
        try
        {
            var items = await _api.GetProductsAsync(_cts.Token);
            await _db.SaveAllProductsAsync(items);
            foreach (var p in items)
                Products.Add(p);
            StatusMessage = $"{Products.Count} productos cargados.";
        }
        catch (OperationCanceledException)
        {
            ErrorMessage = "Operacion cancelada por el usuario.";
            StatusMessage = string.Empty;
        }
        catch (Exception ex)
        {
            var cached = await _db.GetProductsAsync(); 
            if (cached.Count > 0)
            {
                foreach (var p in cached)
                    Products.Add(p);
                StatusMessage = $"{Products.Count} productos cargados desde caché local.";
            }
            else
            {
                ErrorMessage = ex.Message;
                StatusMessage = string.Empty;
            }
        }
        finally
        {
            IsBusy = false;
            _cts?.Dispose();
            _cts = null;
        }
    }

    [RelayCommand]
    private void CancelRequest()
    {
        if (_cts == null || !IsBusy) return;
        _cts.Cancel();
    }

    [RelayCommand]
    private async Task VerDetalle(Product product)
    {
        if (product == null) return;
        var parametros = new Dictionary<string, object>
        {
            ["product"] = product
        };
        await Shell.Current.GoToAsync("detalle", parametros);
    }
}