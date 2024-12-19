using Microsoft.EntityFrameworkCore;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.DbContextMigraciones;
using PedidosApi.Dominio.Persistencia.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;
using System;

namespace PedidosApi.Infraestructura.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IPedidoDbContext _context;

        public ClienteRepositorio(IPedidoDbContext context)
        {
            _context = context;
        }

        public async Task CrearClienteAsync(Cliente cliente)
        {

            var clienteExistente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == cliente.Email);

            if (clienteExistente != null)
            {
               
                throw new Exception("El correo electrónico ya está registrado");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }
    }

}
