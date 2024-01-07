using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public string DbPath { get; }
        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "APICatalogo.db");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }


    }
}
