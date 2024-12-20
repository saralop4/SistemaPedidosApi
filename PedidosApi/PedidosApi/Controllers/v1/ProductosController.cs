﻿using Microsoft.AspNetCore.Mvc;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Controllers.v1
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos([FromQuery] decimal? precioMin, [FromQuery] int? stockMin)
        {
            var productos = await _productoService.ObtenerProductosAsync(precioMin, stockMin);
            return Ok(productos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoDto productoDto)
        {
            await _productoService.ActualizarProductoAsync(id, productoDto);
            return NoContent();
        }
    }
}