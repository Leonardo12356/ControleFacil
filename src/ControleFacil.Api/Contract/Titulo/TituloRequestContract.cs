namespace ControleFacil.Api.Contract
{
    public abstract class TituloRequestContract
    {
        public long IdNaturezaDeLancamento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public double ValorOriginal { get; set; }
        public DateTime? DataReferencia { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}