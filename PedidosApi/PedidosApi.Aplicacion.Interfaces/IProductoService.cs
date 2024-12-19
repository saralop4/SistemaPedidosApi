using PedidosApi.Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosApi.Aplicacion.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerProductosAsync(decimal? precioMin, int? stockMin);
        Task ActualizarProductoAsync(int id, ProductoDto productoDto);
    }
}
