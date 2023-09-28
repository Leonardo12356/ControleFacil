using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface INaturezaDeLancamentoRepository : IRepository<NaturezaDeLancamento, long>
    {
        Task<IEnumerable<NaturezaDeLancamento>> ObeterPeloIdUsuario(long IdUsuario);
    }
}