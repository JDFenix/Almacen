using Microsoft.EntityFrameworkCore;
using AlmacenAIFA.Server.Model;

namespace AlmacenAIFA.Server.Data
{
    public class AlmacenDbContext : DbContext
    {
        public AlmacenDbContext(DbContextOptions<AlmacenDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> categorias { get; set; }
        // Agrega otros DbSet aquí si tienes más modelos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.CorreoElectronico)
                .IsUnique();
        }

    }
}
