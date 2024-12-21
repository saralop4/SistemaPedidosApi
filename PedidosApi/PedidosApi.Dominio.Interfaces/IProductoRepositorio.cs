﻿using PedidosApi.Dominio.Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosApi.Dominio.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<IEnumerable<Producto>> ObtenerProductosConFiltroAsync(decimal? precioMin, int? stockMin);
        Task ActualizarProductoAsync(int id, Producto producto);
        Task<Producto> ObtenerProductoAsync(int id);
    }
}
