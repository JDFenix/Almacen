using AlmacenAIFA.Server.Data;
using AlmacenAIFA.Server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlmacenAIFA.Server.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {


        private AlmacenDbContext _db;

        public ProductoController(AlmacenDbContext db)
        {
            _db = db;
        }


        [Authorize]
        [HttpGet("Listar")]
        public IActionResult Index()
        {

            try
            {
                List<Producto> productos = _db.Productos.ToList();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los productos: {ex.Message}");
            }
        }


        [Authorize]
        [HttpPost("Crear")]
        public IActionResult Crear([FromBody] Producto producto)
        {
            try
            {
                if (producto == null || producto.Nombre == null || producto.Stock <= 0)
                {
                    return BadRequest("Producto Invalido");
                }

                _db.Productos.Add(producto);
                _db.SaveChanges();
                return StatusCode(201, "Producto registrado correctamente");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar un producto {ex.Message}");
            }
        }


        [Authorize]
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest($"Producto inválido {ModelState}");
                }

                if (producto == null || producto.Id <= 0)
                {
                    return BadRequest("Producto inválido");
                }

                var existente = await _db.Productos.FindAsync(producto.Id);
                if (existente == null)
                    return NotFound($"Producto {producto.Id} no encontrado.");

                existente.Nombre = producto.Nombre;
                existente.Stock = producto.Stock;
                existente.QrCode = producto.QrCode;

                await _db.SaveChangesAsync();
                return Ok(existente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar un producto {ex.Message}");
            }
        }


        [Authorize]
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(long productoId)
        {
            try
            {
                if (productoId <= 0)
                {
                    return BadRequest("Producto invalido");
                }

                var producto = await _db.Productos.FindAsync(productoId);
                if (producto == null)
                {
                    return BadRequest($"Producto {productoId} no encontrado");
                }

                _db.Productos.Remove(producto);
                await _db.SaveChangesAsync();

                return Ok("Producto eliminado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al eliminar el producto {productoId}");
            }
        }
    }
}
