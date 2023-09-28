using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface IApagarRepository : IRepository<Apagar, long>
    {
        Task<IEnumerable<Apagar>> ObeterPeloIdUsuario(long IdUsuario);
    }
}