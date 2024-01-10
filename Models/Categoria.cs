using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    public class Categoria
    {
        Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImgUrl { get; set; }
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }

        public Categoria(int id, string nome, string imgUrl)
        {
            Id = id;
            Nome = nome;
            ImgUrl = imgUrl;
        }
    }
}
