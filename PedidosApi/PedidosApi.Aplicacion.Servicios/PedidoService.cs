using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Aplicacion.Servicios
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepositorio _repositorio;

        private readonly IProductoRepositorio _repositorioProducto;

        public PedidoService(IPedidoRepositorio repositorio, IProductoRepositorio repositorioProducto)
        {
            _repositorio = repositorio;
            _repositorioProducto = repositorioProducto;

        }


        public async Task CrearPedidoAsync(PedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                ClienteId = pedidoDto.ClienteId,
                FechaPedido = DateTime.Now
            };


            foreach (var productoDto in pedidoDto.Productos)
            {
                var producto = await _repositorioProducto.ObtenerProductoAsync(productoDto.ProductoId);
                if (producto == null || producto.Stock < productoDto.Cantidad)
                {
                    throw new StockInsuficienteException("Stock insuficiente para el producto: " + productoDto.ProductoId);
                }

                pedido.PedidoProductos.Add(new PedidoProducto
                {
                    PedidoId = pedido.Id,
                    ProductoId = productoDto.ProductoId,
                    Cantidad = productoDto.Cantidad
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
