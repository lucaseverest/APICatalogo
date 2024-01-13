using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=APICatalogo.db");

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categoria
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
            modelBuilder.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(255).IsRequired();

            //Produto
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(255);
            modelBuilder.Entity<Produto>().Property(p => p.ImgUrl).HasMaxLength(255);
            modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(14, 2);

            //Relacionamento
            modelBuilder.Entity<Produto>()
                .HasOne<Categoria>(c => c.Categoria)
                    .WithMany(p => p.Produtos)
                        .HasForeignKey(c => c.Id);
        }
    }
}
