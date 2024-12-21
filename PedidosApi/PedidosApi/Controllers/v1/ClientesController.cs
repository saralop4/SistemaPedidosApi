using Microsoft.AspNetCore.Mvc;
using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Controllers.V1
{
    [Route("Api/V1/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDto clienteDto)
        {
            try
            {
                await _clienteService.CrearClienteAsync(clienteDto);
                var message = "cliente guardado  exitosamente.";
                return Ok(new { mensaje = message });
            }
            catch (DuplicateEmailException ex)
            {

                return BadRequest(new { mensaje = "El correo electrónico ya está en uso" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al crear cliente: {ex.Message}");

                return BadRequest(new { mensaje = " Se ha producido un error al crear el cliente. Compruebe los datos de la solicitud e inténtelo de nuevo." });
            }


        }
    }

}
