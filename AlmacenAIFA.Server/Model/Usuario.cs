using System.ComponentModel.DataAnnotations;

namespace AlmacenAIFA.Server.Model
{
    public class Usuario
    {
        [Required(ErrorMessage = "ID no puede ser null")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Nombre no puede ser null")]
        public string Nombre { get; set; } = string.Empty;


        [Required(ErrorMessage = "Apellido Paterno no puede ser null")]
        public string ApellidoPaterno { get; set; } = string.Empty;


        [Required(ErrorMessage = "Apellido Materno no puede ser null")]
        public string ApellidoMaterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contrasena no puede ser null")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "CorreoElectronico no puede ser null")]
        [MaxLength(250)]
        public string CorreoElectronico { get; set; } = string.Empty;

        public Rol? Rol { get; set; }

        [Required(ErrorMessage = "RolId no puede ser null")]
        public long RolId { get; set; }

    }
}
