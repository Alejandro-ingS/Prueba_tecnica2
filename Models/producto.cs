//Using System.componentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_tecnica.Models
{
    public class producto
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        public string descripcion { get; set; }
        [Required]

        [Column(TypeName = "decimal(10,2)")]
        public decimal precio { get; set; }
        [Required]

        public int cantidad { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<productoCategoria> productoCategorias { get; set; } = new List<productoCategoria>();


    }
}
