using Microsoft.EntityFrameworkCore;
using PedidosApi.Dominio.Dtos;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Dominio.Persistencia.Interfaces
{
    public interface IPedidoDbContext
    {

        public  DbSet<Cliente> Clientes { get; set; }

        public  DbSet<Pedido> Pedidos { get; set; }

        public  DbSet<PedidoProducto> PedidoProductos { get; set; }

        public  DbSet<Producto> Productos { get; set; }

        public DbSet<VistaTotalPedidoDto> VistaTotalPedidos { get; set; } // Agregamos esta propiedad

        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();

    }
}
