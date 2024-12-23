using Microsoft.AspNetCore.Mvc;
using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Controllers.v1
{
    [Route("Api/V1/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDto pedidoDto)
        {
            try { 
            await _pedidoService.CrearPedidoAsync(pedidoDto);
            return Ok(new { mensaje = "Pedido Creado Exitosamente." });
             }
            catch (StockInsuficienteException ex)
            {

                return BadRequest(new { mensaje = "Stock insuficiente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = " Se ha producido un error al crear el cliente. Compruebe los datos de la solicitud e inténtelo de nuevo." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            var pedido = await _pedidoService.ObtenerPedidoAsync(id);
            if (pedido == null)
            {
                return NotFound(new { mensaje = "No se encontró el pedido" });
            }

            return Ok(pedido);
        }

        //[HttpGet("{id}/total")]
        //public async Task<IActionResult> ObtenerTotalPedido(int id)
        //{
        //    var total = await _pedidoService.ObtenerTotalPedidoAsync(id);
        //    if (total == null)
        //    {
        //        return NotFound(new { mensaje = "No se encontrarón  pedidos" });
        //    }

        //    return Ok(total);
        //}
    }
}