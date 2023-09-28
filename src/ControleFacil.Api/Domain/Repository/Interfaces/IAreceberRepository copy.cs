using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface IAreceberRepository : IRepository<Areceber, long>
    {
        Task<IEnumerable<Areceber>> ObeterPeloIdUsuario(long IdUsuario);
    }
}