using System.ComponentModel.DataAnnotations;

namespace ControleFacil.Api.Domain.Models
{
    public class Apagar : Titulo
    {
        [Required(ErrorMessage = "O campo Valor Pago é obrigatório.")]
        public double ValorPago { get; set; }

        public DateTime? DataPagamento { get; set; }
    }
}