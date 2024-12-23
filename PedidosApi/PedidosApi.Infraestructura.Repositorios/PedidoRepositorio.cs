﻿using Microsoft.EntityFrameworkCore;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Infraestructura.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {

        private readonly IPedidoDbContext _context;

        public PedidoRepositorio(IPedidoDbContext context)
        {
            _context = context;
        }

        public async Task CrearPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido?> ObtenerPedidoAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente) 
                .Include(p => p.PedidoProductos)
                    .ThenInclude(pp => pp.Producto) 
                .FirstOrDefaultAsync(p => p.Id == id); 
        }


        public async Task<decimal?> ObtenerTotalPedidoVista(int id)
        {
            return await _context.VistaTotalPedidos
                .Where(vtp => vtp.PedidoId == id)
                .Select(vtp => vtp.Total)
                .FirstOrDefaultAsync();
        }
    }
}