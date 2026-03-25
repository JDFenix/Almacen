using System.ComponentModel.DataAnnotations;

namespace AlmacenAIFA.Server.Model
{
    public class Categoria
    {

        [Required(ErrorMessage = "ID no puede ser null")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Nombre no puede ser null")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;



    }
}
