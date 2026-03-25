using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AlmacenAIFA.Server.Model.DTO
{
    public class UsuarioDTO 
    {

        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public Rol? Rol { get; set; }
        public long RolId { get; set; }
    }
}
