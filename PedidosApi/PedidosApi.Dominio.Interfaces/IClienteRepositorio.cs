using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Dominio.Interfaces
{
    public interface IClienteRepositorio
    {
        Task CrearClienteAsync(Cliente cliente);
    }
}
