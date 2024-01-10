using APICatalogo.Models;

namespace APICatalogo.Context
{
    public class Seeding
    {
        private AppDbContext _context;
        public Seeding(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Categorias.Any() || _context.Produtos.Any()) return;

            Categoria c1 = new Categoria(1, "Carros", "Carro.png");
            Categoria c2 = new Categoria(2, "Bikes", "Bikes.png");
            Categoria c3 = new Categoria(3, "Imoveis", "Imoveis.png");

            Produto p1 = new Produto(1, "Ford", "carrão", 10, "carrao.png", 2, new DateTime(), 1);
            Produto p2 = new Produto(2, "Ford", "carrão", 10, "carrao.png", 2, new DateTime(), 1);
            Produto p3 = new Produto(3, "Ford", "carrão", 10, "carrao.png", 2, new DateTime(), 1);

            _context.Categorias.AddRange(c1, c2, c3);
            _context.Produtos.AddRange(p1, p2, p3);
            _context.SaveChanges();
        }
    }
}
