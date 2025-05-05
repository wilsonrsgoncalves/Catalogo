using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("produtos")]
public class Produto : IValidatableObject
{
    [Key]
    [Column("id")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} e no mínimo {2} caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    [Column("nome")]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    [Column("descricao")]
    public string? Descricao { get; set; }

    [Required]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    [Column("preco", TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10)]
    [Column("imagem_url")]
    public string? ImagemUrl { get; set; }

    [Column("estoque")]
    public float Estoque { get; set; }

    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; }

    [Column("categoria_id")]
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                string[] memberNames = new[] { nameof(this.Nome) };
                yield return new ValidationResult("A primeira letra do produto deve ser maiúscula",
                    memberNames);
            }
        }

        if (this.Estoque <= 0)
        {
            string[] memberNames = new[] { nameof(this.Estoque) };
            yield return new ValidationResult("O estoque deve ser maior que zero",
                memberNames);
        }
    }
}