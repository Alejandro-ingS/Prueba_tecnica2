namespace Prueba_tecnica.Models
{
    public class categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<productoCategoria> productoCategorias { get; set; } = new List<productoCategoria>();
    }
}
