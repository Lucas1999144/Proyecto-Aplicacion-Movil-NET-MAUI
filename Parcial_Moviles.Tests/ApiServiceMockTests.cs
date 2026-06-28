using Moq;
using Parcial_Moviles.Models;
using Parcial_Moviles.Services;
using System.Reflection;

namespace Parcial_Moviles.Tests;

public class ApiServiceMockTests
{
    private readonly Mock<IApiService> _mockApi;

    public ApiServiceMockTests()
    {
        _mockApi = new Mock<IApiService>();
    }

    [Fact]
    public async Task GetProductsAsync_RetornaListaProductos()
    {
        /// Arrange
        var productoEsperado = new List<Product>
        {
            new Product
            {
                Id = 1,
                Title = "Remera",
                Price = 100m
            },
            new Product
            {
                Id = 2,
                Title = "Pantalon",
                Price = 200m
            }
        };
        _mockApi.Setup(a => a.GetProductsAsync(default)).ReturnsAsync(productoEsperado);

        /// Act
        var result = await _mockApi.Object.GetProductsAsync();

        /// Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetProductByIdAsync_IdExiste_RetornaProducto()
    {
        /// Le tengo que pedir al mock el producto con id 1 - Verifico si el Title esta OK
        var product = new Product
        {
            Id = 1,
            Title = "Remera",
            Price = 100m
        };
        _mockApi.Setup(a => a.GetProductByIdAsync(1, default)).ReturnsAsync(product);

        var resultado = await _mockApi.Object.GetProductByIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal("Remera", resultado.Title);
    }

    [Fact]
    public async Task GetProductByIdAsync_IdNoExiste_RetornaNull()
    {
        /// Le pido Id 999 - tiene que devolver null
        _mockApi.Setup(a => a.GetProductByIdAsync(999, default)).ReturnsAsync((Product?)null);

        var result = await _mockApi.Object.GetProductByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetProductsAsync_Falla_LanzaExcept()
    {
        /// Simula que la API falla por conexion a internet.
        _mockApi.Setup(a => a.GetProductsAsync(default)).ThrowsAsync(new Exception("Sin conexion."));

        var excp = await Assert.ThrowsAsync<Exception>(() => _mockApi.Object.GetProductsAsync());

        Assert.Contains("Sin conexion", excp
            .Message);
    }
}