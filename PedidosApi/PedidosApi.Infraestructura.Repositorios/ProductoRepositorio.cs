using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Producto>> ObtenerProductosAsync(decimal? precioMin, int? stockMin)
        {
            return await _context.Productos
                .Where(p => (!precioMin.HasValue || p.Precio >= precioMin) &&
                            (!stockMin.HasValue || p.Stock >= stockMin))
                .ToListAsync();
        }

        public async Task ActualizarProductoAsync(int id, Producto producto)
        {
            var existente = await _context.Productos.FindAsync(id);
            if (existente != null)
            {
                existente.Precio = producto.Precio;
                existente.Stock = producto.Stock;
                await _context.SaveChangesAsync();
            }
        }
    }

}
