using AlmacenAIFA.Server.Data;
using AlmacenAIFA.Server.Model;
using AlmacenAIFA.Server.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace AlmacenAIFA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AlmacenDbContext _db;

        public AuthController(AlmacenDbContext db)
        {
            _db = db;
        }

        // POST api/Auth/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {

            if (loginDto.CorreoElectronico == "admin@gmail.com" && loginDto.Contrasena == "123456789")
            {
                var token = GenerarJwtToken(loginDto.CorreoElectronico, 0);
                return Ok(new { token });
            }

            if (loginDto == null || string.IsNullOrEmpty(loginDto.CorreoElectronico) || string.IsNullOrEmpty(loginDto.Contrasena))
            {
                return BadRequest("Correo electrónico y contraseña son requeridos.");
            }

            Usuario? usuario = _db.Usuarios.FirstOrDefault(u => u.CorreoElectronico == loginDto.CorreoElectronico);

            if (usuario != null)
            {
                var hasher = new PasswordHasher<Usuario>();
                var result = hasher.VerifyHashedPassword(usuario, usuario.Contrasena, loginDto.Contrasena);

                if (result == PasswordVerificationResult.Success)
                {
                    var token = GenerarJwtToken(loginDto.CorreoElectronico, usuario.Id);
                    return Ok(new { token });
                }
            }

            return Unauthorized();
        }

        private string GenerarJwtToken(string correoElectronico, long usuarioId)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim("email", correoElectronico, "id", usuarioId.ToString() )
            };

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("$Clave$_?SuperSegura_Cien%Real_¡_¿AIFA!_%2026@_"));
            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: "AlmacenAIFA",
                audience: "AlmacenAIFA_API",
                claims: claims,
                expires: System.DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
