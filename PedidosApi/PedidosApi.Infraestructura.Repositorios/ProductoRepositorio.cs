using Microsoft.EntityFrameworkCore;
using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Infraestructura.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly IPedidoDbContext _context;

        public ProductoRepositorio(IPedidoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosConFiltroAsync(decimal? precioMin, int? stockMin)
        {
            return await _context.Productos
                .Where(p => (!precioMin.HasValue || p.Precio >= precioMin) &&
                            (!stockMin.HasValue || p.Stock >= stockMin))
                .ToListAsync();
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<Producto> ObtenerProductoAsync(int id)
        {
            var existente = await _context.Productos.FindAsync(id);

            return existente;
        }
    }

}
