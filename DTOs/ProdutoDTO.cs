using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public float Preco { get; set; }
        public string? ImgUrl { get; set; }
        public float Estoque { get; set; }
        public int CategoriaId { get; set; }
    }
}
