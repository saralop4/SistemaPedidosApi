using PedidosApi.Dominio.Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosApi.Dominio.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task CrearPedidoAsync(Pedido pedido);
        Task<Pedido?> ObtenerPedidoAsync(int id);
        Task<decimal?> ObtenerTotalPedidoVista(int id);
    }
}
