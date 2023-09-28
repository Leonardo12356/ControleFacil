namespace ControleFacil.Api.Contract.Usuario
{
    public class UsuarioResponseContract : UsuarioRequestContract
    {
        public long Id { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}