using Parcial_Moviles.Models;
using Parcial_Moviles.Services;

namespace Parcial_Moviles.Tests;

public class DatabaseServicesTests
{
    private DatabaseService crearDb()
    {
        return new DatabaseService(":memory:");
    }

    [Fact]
    public async Task GuardarProducto_ObtenerTodos_RetornaProductoGuardado()
    {
        var db = crearDb();
        var product = new Product();
        product.Id = 1;
        product.Title = "Remera";
        product.Price = 100m;

        await db.SaveProductAsync(product);
        var products = await db.GetProductsAsync();

        Assert.Single(products);
        Assert.Equal("Remera", products[0].Title);
    }
    [Fact]
    public async Task ObtenerProductosPorId_IdExiste_RetornaProducto()
    {
        var db = crearDb();
        var p = new Product();
        p.Id = 1;
        p.Title = "Remera";
        p.Price = 100m;
        await db.SaveProductAsync(p);

        var r = await db.GetProductByIdAsync(1);

        Assert.NotNull(r);
        Assert.Equal("Remera", r.Title);
    }
    [Fact]
    public async Task ObtenerProductoId_IdNoExiste_RetornaNull()
    {
        var db = crearDb();

        var result = await db.GetProductByIdAsync(999);

        Assert.Null(result);
    }

    /// Validaciones
    [Fact]
    public async Task SaveProductsAsync_TituloVacio_LanzaExcept()
    {
        var db = crearDb();
        var product = new Product();
        product.Id = 001;
        product.Title = "";
        product.Price = 100m;

        await Assert.ThrowsAsync<ArgumentException>(() =>
        db.SaveProductAsync(product));
    }

    [Fact]
    public async Task SaveProductsAsync_PrecioNegativo_LanzarExcept()
    {
        var db = crearDb();
        var product = new Product
        {
            Id = 002,
            Title = "Remera",
            Price = -100m
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
        db.SaveProductAsync(product));
    }

    [Fact]
    public async Task SaveProductsAsync_ProductoNulo_LanzarExcept()
    {
        var db = crearDb();

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
        db.SaveProductAsync(null!));
    }

    [Fact]
    public async Task GetProductByIdAsync_IdCero_LanzarExcept()
    {
        var db = crearDb();

        await Assert.ThrowsAsync<ArgumentException>((() =>
        db.GetProductByIdAsync(0)));
    }
}