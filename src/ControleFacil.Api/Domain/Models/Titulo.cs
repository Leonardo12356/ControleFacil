using System.ComponentModel.DataAnnotations;

namespace ControleFacil.Api.Domain.Models
{
    public abstract class Titulo
    {


        [Key]
        public long Id { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        [Required]
        public long IdNaturezaDeLancamento { get; set; }

        public NaturezaDeLancamento? NaturezaDeLancamento { get; set; }

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Valor Original é obrigatório.")]
        public double ValorOriginal { get; set; }
        public string? Observacao { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo Data Vencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }
        public DateTime? DataInativacao { get; set; }

        public DateTime? DataReferencia { get; set; }



    }
}