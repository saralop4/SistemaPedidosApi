namespace PedidosApi.Dominio.Dtos
{

    public class PedidoProductoDetalleDto
        {

            public string? NombreProducto { get; set; }
            public decimal PrecioUnitario { get; set; }
            public int Cantidad { get; set; }

        }
    
}
