using PedidosApi.Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosApi.Aplicacion.Interfaces
{
    public interface IClienteService
    {
        Task CrearClienteAsync(ClienteDto clienteDto);
    }
}
