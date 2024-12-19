using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Aplicacion.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepositorio _repositorio;

        public ProductoService(IProductoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync(decimal? precioMin, int? stockMin)
        {
            var productos = await _repositorio.ObtenerProductosAsync(precioMin, stockMin);
            return productos.Select(p => new ProductoDto
            {
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock
            });
        }

        public async Task ActualizarProductoAsync(int id, ProductoDto productoDto)
        {
            await _repositorio.ActualizarProductoAsync(id, new Producto
            {
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            });
        }
    }
}
