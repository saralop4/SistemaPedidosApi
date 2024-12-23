namespace PedidosApi.Dominio.Dtos
{
    public class PedidoDetalleDto
    {
        public string? NombreCliente { get; set; }

        public DateTime FechaPedido { get; set; }

        public List<PedidoProductoDetalleDto> Productos { get; set; } = new();

    }
}
