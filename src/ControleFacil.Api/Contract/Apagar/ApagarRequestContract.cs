namespace ControleFacil.Api.Contract.Apagar
{
    public class ApagarRequestContract : TituloRequestContract
    {
        public double ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}