using System.Collections.ObjectModel;

namespace APICatalogo.Models
{
    public class Categoria
    {
        Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? ImgUrl { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
