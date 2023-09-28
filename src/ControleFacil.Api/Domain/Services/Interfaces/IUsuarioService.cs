using ControleFacil.Api.Contract.Usuario;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, long>
    {
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest);

        Task<UsuarioResponseContract> ObterEmail(string email);
    }
}