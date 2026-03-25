using AlmacenAIFA.Server.Data;
using AlmacenAIFA.Server.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenAIFA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly AlmacenDbContext _db;

        public RolController(AlmacenDbContext db)
        {
            _db = db;
        }



        // GET: RolController/Crear

        [Authorize]
        [HttpPost("Crear")]
        public IActionResult Crear([FromBody] Rol rol)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _db.Roles.Add(rol);
                _db.SaveChanges();
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el rol {ex.Message}");
            }
        }

    
    }
}
