using Microsoft.AspNetCore.Mvc;
using PedidosApi.Aplicacion.Exceptions;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Controllers.v1
{
    [Route("Api/V1/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductosConFiltroAsync([FromQuery] decimal? precioMin, [FromQuery] int? stockMin)
        {
            var productos = await _productoService.ObtenerProductosConFiltroAsync(precioMin, stockMin);
            return Ok(productos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoDto productoDto)
        {
            try
            {
                await _productoService.ActualizarProductoAsync(id, productoDto);
                return Ok(new { mensaje = "Actualizacion Exitosa" }); 
            }
            catch (ProductoNoEncontradoException)
            {
                return NotFound(new { mensaje = "Producto no encontrado" }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = " Se ha producido un error. Compruebe los datos de la solicitud e inténtelo de nuevo." });
            }
        }

    }
}