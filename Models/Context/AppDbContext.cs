using Microsoft.EntityFrameworkCore;

namespace back
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteArticulo> ClientesArticulos { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<TiendaArticulo> TiendasArticulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}