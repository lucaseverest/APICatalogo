using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(80)]
        public string? Descricao { get; set; }
        public float Preco { get; set; }

        [Required]
        [StringLength(80)]
        public string? ImgUrl { get; set; }
        public float Estoque { get; set; }
        public int CategoriaId { get; set; }
    }
}
