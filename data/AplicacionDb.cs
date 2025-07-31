namespace Prueba_tecnica.data
{
    public class AplicacionDb
    {
        public AplicacionDb(DbContextOptions<AplicacionDb> options) : base(options)
        {
        }

        public DbSet<Prueba_tecnica.Models.producto> Productos { get; set; }
        public DbSet<Prueba_tecnica.Models.categoria> Categorias { get; set; }
        public DbSet<Prueba_tecnica.Models.ProductoCategoria> ProductoCategorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prueba_tecnica.Models.ProductoCategoria>()
                .HasKey(pc => new { pc.ProductoId, pc.CategoriaId });
            modelBuilder.Entity<Prueba_tecnica.Models.ProductoCategoria>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.productoCategorias)
                .HasForeignKey(pc => pc.ProductoId);
            modelBuilder.Entity<Prueba_tecnica.Models.ProductoCategoria>()
                .HasOne(pc => pc.Categoria)
                .WithMany(c => c.productoCategorias)
                .HasForeignKey(pc => pc.CategoriaId);
        }
    }
}
