namespace PedidosApi.Dominio.Dtos
{
    public class PedidoDto
    {
        public int ClienteId { get; set; }
        public List<PedidoProductoDto> Productos { get; set; } = new();
    }
}
