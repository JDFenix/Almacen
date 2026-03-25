using System.ComponentModel.DataAnnotations;

namespace AlmacenAIFA.Server.Model
{
    public class Producto
    {

        [Required(ErrorMessage = "ID no puede ser null")]
        public long Id { get; set; }


        [Required(ErrorMessage = "Nombre no puede ser null")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stock no puede ser null")]
        public int Stock { get; set; }
        public string QrCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Categoria no puede ser null")]
        public long CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }

}
