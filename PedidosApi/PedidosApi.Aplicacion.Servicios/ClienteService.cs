using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.Modelos;

namespace PedidosApi.Aplicacion.Servicios
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepositorio _repositorio;

        public ClienteService(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task CrearClienteAsync(ClienteDto clienteDto)
        {
            try
            {
                await _repositorio.CrearClienteAsync(new Cliente
                {
                    Nombre = clienteDto.Nombre,
                    Email = clienteDto.Email,
                    FechaRegistro = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                
                throw new DuplicateEmailException("El correo electrónico ya está en uso");
            }
        }
    }
}
