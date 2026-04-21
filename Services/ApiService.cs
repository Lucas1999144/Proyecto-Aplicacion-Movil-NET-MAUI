using Parcial_Moviles.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Parcial_Moviles.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http)
    {
        _http = http;
        _http.BaseAddress ??= new Uri("https://fakestoreapi.com/");
    }

    public async Task<List<Product>> GetProductsAsync(CancellationToken ct = default)
    {   // Manejo de errores a la hora de obtener productos.
        try
        {
            var products = await _http.GetFromJsonAsync<List<Product>>("products", ct);
            return products ?? new List<Product>();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new Exception("Error 404: No se encontro el recurso.", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            throw new Exception("Error 500: Problemas en el servidor.", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception("Error 400: Solicitud incorrecta.", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == null)
        {
            throw new Exception("Sin conexion a internet. Verifica tu red.", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("No se pudo interpretar la respuesta de la API.", ex);
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken ct = default)
    {   // Manejo de errores cuando consigo un producto por id. 
        try
        {
            return await _http.GetFromJsonAsync<Product>($"products/{id}", ct);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new Exception($"Error 404: Producto con Id {id} no encontrado.", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            throw new Exception("Error 500: Problemas en el servidor.", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == null)
        {
            throw new Exception("Sin conexion a internet.", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("No se pudo interpretar la respuesta de la API.", ex);
        }
    }
}