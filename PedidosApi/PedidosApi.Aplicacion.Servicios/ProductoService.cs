using PedidosApi.Aplicacion.Exceptions;
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

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosConFiltroAsync(decimal? precioMin, int? stockMin)
        {
            var productos = await _repositorio.ObtenerProductosConFiltroAsync(precioMin, stockMin);
            return productos.Select(p => new ProductoDto
            {
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock
            });
        }

        public async Task ActualizarProductoAsync(int id, ProductoDto productoDto)
        {
            // Obtener el producto existente
            var productoExistente = await _repositorio.ObtenerProductoAsync(id);
            if (productoExistente == null)
            {
                throw new ProductoNoEncontradoException($"El producto con ID {id} no existe.");
            }

            // Actualizar las propiedades
            productoExistente.Nombre = productoDto.Nombre;
            productoExistente.Precio = productoDto.Precio;
            productoExistente.Stock = productoDto.Stock;

            // Guardar cambios
            await _repositorio.ActualizarProductoAsync(productoExistente);
        }


        public async Task<ProductoDto> ObtenerProductoAsync(int id)
        {
            var producto = await _repositorio.ObtenerProductoAsync(id);

            if (producto == null)
            {
                throw new Exception();
            }
            return new ProductoDto
            {
                
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }
    }
}
