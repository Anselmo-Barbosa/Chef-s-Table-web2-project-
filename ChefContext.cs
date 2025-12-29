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
            modelBuilder.Entity<Avaliacao>()
           .HasOne(a => a.Usuario)
           .WithMany(u => u.Avaliacoes)
           .HasForeignKey(a => a.UsuarioId)
           //Se apagar um usuário, não apague automaticamente avaliações
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comentario>()
           .HasOne(c => c.Usuario)
           .WithMany(u => u.Comentarios)
           .HasForeignKey(c => c.UsuarioId)
           //Se apagar um usuário, não apague automaticamente comentarios
           .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Receita>().ToTable("Receita");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacao");
            modelBuilder.Entity<Comentario>().ToTable("Comentario");
            modelBuilder.Entity<Foto>().ToTable("Fotos");
        }
    }
}
