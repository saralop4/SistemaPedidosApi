using Microsoft.AspNetCore.Mvc;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Controllers.v1
{
    [Route("Api/[controller]")]
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
            await _pedidoService.CrearPedidoAsync(pedidoDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            var pedido = await _pedidoService.ObtenerPedidoAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet("{id}/total")]
        public async Task<IActionResult> ObtenerTotalPedido(int id)
        {
            var total = await _pedidoService.ObtenerTotalPedidoAsync(id);
            if (total == null)
            {
                return NotFound();
            }

            return Ok(total);
        }
    }
}