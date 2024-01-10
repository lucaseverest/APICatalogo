using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImgUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        public static explicit operator Produto(NotFoundObjectResult v)
        {
            throw new NotImplementedException();
        }

        public Produto(int id, string nome, string descricao, decimal preco, string imgUrl, float estoque, DateTime dataCadastro, int categoriaId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImgUrl = imgUrl;
            Estoque = estoque;
            DataCadastro = dataCadastro;
            CategoriaId = categoriaId;
        }
    }
}
