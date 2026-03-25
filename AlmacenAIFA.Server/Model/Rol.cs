using System.ComponentModel.DataAnnotations;

namespace AlmacenAIFA.Server.Model
{
    public class Rol
    {
        [Required(ErrorMessage = "ID no puede ser null")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Stock no puede ser null")]
        public string Nombre { get; set; } = string.Empty;

    }
}
