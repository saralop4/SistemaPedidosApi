using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Aplicacion.Interfaces
{
    public interface IPedidoService
    {
        Task CrearPedidoAsync(PedidoDto pedidoDto);
        Task<PedidoDetalleDto?> ObtenerPedidoAsync(int id);
        Task<decimal?> ObtenerTotalPedidoAsync(int id);
    }
}
