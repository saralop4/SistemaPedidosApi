using Microsoft.EntityFrameworkCore;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Infraestructura.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IPedidoDbContext _context;

        public ClienteRepositorio(IPedidoDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObtenerClientePorEmailAsync(string email)
        {
           
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);

            return cliente;
        }

        public async Task CrearClienteAsync(Cliente cliente)
        {

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

    }

}
