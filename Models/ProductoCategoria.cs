namespace Prueba_tecnica.Models
{
    public class ProductoCategoria
    {
        public int ProductoId { get; set; }
        public producto Producto { get; set; }
        public int CategoriaId { get; set; }
        public categoria Categoria { get; set; }

    }
}
