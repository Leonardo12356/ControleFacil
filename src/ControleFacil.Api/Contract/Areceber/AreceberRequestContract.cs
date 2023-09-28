namespace ControleFacil.Api.Contract.Areceber
{
    public class AreceberRequestContract : TituloRequestContract
    {
        public double ValorRecebido { get; set; }
        public DateTime? DataRecebimento { get; set; }
    }
}