﻿namespace PedidosApi.Dominio.Dtos
{
    public class ProductoDto
    {
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

    }
}
