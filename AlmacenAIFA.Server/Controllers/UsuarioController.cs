using AlmacenAIFA.Server.Data;
using AlmacenAIFA.Server.Model;
using AlmacenAIFA.Server.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AlmacenAIFA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly AlmacenDbContext _db;


        public UsuarioController(AlmacenDbContext db)
        {
            _db = db;
        }


        [Authorize]
        [HttpGet("Listar")]
        public IActionResult Index()
        {
            try
            {
                var usuarios = _db.Usuarios
                          .Select(u => new UsuarioDTO
                          {
                              Id = u.Id,
                              Nombre = u.Nombre,
                              ApellidoPaterno = u.ApellidoPaterno,
                              ApellidoMaterno = u.ApellidoMaterno,
                              CorreoElectronico = u.CorreoElectronico,
                              RolId = u.RolId
                          })
                          .ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los usuarios: {ex.Message}");
            }
        }




        [Authorize]
        [HttpPost("Crear")]
        public IActionResult Crear([FromBody] Usuario usuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var hasher = new PasswordHasher<Usuario>();
                usuario.Contrasena = hasher.HashPassword(usuario, usuario.Contrasena);

                _db.Usuarios.Add(usuario);
                _db.SaveChanges();

                return StatusCode(201, "Usuario Creado Correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear un usuario ${ex.Message}");
            }


        }

    }
}
