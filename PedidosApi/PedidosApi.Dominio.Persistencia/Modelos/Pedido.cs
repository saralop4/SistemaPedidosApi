using System;
using System.Collections.Generic;

namespace PedidosApi.Dominio.Persistencia.Modelos;

public partial class Pedido
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public DateTime FechaPedido { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();
}
