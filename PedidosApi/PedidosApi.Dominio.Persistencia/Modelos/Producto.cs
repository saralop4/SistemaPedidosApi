using System;
using System.Collections.Generic;

namespace PedidosApi.Dominio.Persistencia.Modelos;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();
}
