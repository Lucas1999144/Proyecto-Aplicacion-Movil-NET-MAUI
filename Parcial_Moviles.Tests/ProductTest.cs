using Parcial_Moviles.Models;

namespace Parcial_Moviles.Tests
{
    public class ProductTest
    {
        [Fact]
        public void EsValido_TituloYPrecioValido_esTrue()
        {
            var product = new Product();
            product.Id = 00000001;
            product.Title = "Producto de prueba";
            product.Price = 10.0m;
            product.Category = "Categoria de prueba";
            product.Description = "Descripcion de prueba";

            bool result = product.EsValido();

            Assert.True(result);
        }
        [Fact]
        public void EsValido_TituloVacio_EsFalse()
        {
            var product = new Product();
            product.Title = "";
            product.Price = 10.0m;

            Assert.False(product.EsValido());
        }
        [Fact]
        public void EsValido_PrecioCero_EsFalse()
        {
            var product = new Product();
            product.Title = "Remera";
            product.Price = 0.0m;

            Assert.False(product.EsValido());
        }

        [Theory]
        [InlineData("Producto de prueba", 10.0, true)]
        [InlineData("", 10.0, false)]
        [InlineData("Remera", 0.0, false)]

        public void EsValido_Varios(string title, decimal price, bool expected)
        {
            var product = new Product();
            product.Id = 00000001;
            product.Title = title;
            product.Price = price;

            Assert.Equal(expected, product.EsValido());
        }

        [Fact]
        public void ObtenerInformacion_DatosCompletos_TieneTitulo()
        {
            var product = new Product();
            product.Id = 00000001;
            product.Title = "Remera";
            product.Price = 10.0m;

            string info = product.ObtenerInformacion();

            Assert.Contains("Remera", info);
        }
    }
    
}
