using FluentValidation;
using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Aplicacion.Validadores;
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
 
            var validator = new ClienteDtoValidator();
            var validationResult = validator.Validate(clienteDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var clienteExistente = await _repositorio.ObtenerClientePorEmailAsync(clienteDto.Email);
            if (clienteExistente != null)
            {
                throw new DuplicateEmailException();
            }

            await _repositorio.CrearClienteAsync(new Cliente
            {
                Nombre = clienteDto.Nombre,
                Email = clienteDto.Email,
                FechaRegistro = DateTime.Now
            });
        }
    }
}
