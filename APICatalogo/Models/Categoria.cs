using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace APICatalogo.Models;

[Table("categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = []; 
    }

    [Key]
    [Column("id")]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    [Column("nome")]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    [Column("imagem_url")]
    public string? ImagemUrl { get; set; }

    [JsonIgnore]
    public ICollection<Produto>? Produtos { get; set; }
}