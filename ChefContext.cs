using ChefsTable.Models;
using Microsoft.EntityFrameworkCore;


namespace ChefsTable
{
    public class ChefContext : DbContext
    {
        public ChefContext(DbContextOptions<ChefContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Receita>().ToTable("Receita");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacao");
            modelBuilder.Entity<Comentario>().ToTable("Comentario");
        }
    }
}
