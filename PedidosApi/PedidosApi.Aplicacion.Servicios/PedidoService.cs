using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Aplicacion.Servicios
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepositorio _repositorio;

        public PedidoService(IPedidoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task CrearPedidoAsync(PedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                ClienteId = pedidoDto.ClienteId,
                FechaPedido = DateTime.Now
            };

            foreach (var producto in pedidoDto.Productos)
            {
                pedido.PedidoProductos.Add(new PedidoProducto
                {
                    ProductoId = producto.ProductoId,
                    Cantidad = producto.Cantidad
                });
            }

            await _repositorio.CrearPedidoAsync(pedido);
        }

        public async Task<PedidoDto?> ObtenerPedidoAsync(int id)
        {
            var pedido = await _repositorio.ObtenerPedidoAsync(id);
            if (pedido == null)
            {
                return null;
            }

            return new PedidoDto
            {
                ClienteId = pedido.ClienteId,
                Productos = pedido.PedidoProductos.Select(pp => new PedidoProductoDto
                {
                    ProductoId = pp.ProductoId,
                    Cantidad = pp.Cantidad
                }).ToList()
            };
        }

        public async Task<decimal?> ObtenerTotalPedidoAsync(int id)
        {
            return await _repositorio.ObtenerTotalPedidoVista(id);
        }
    }
}
