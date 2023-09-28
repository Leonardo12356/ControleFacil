using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{
    public class NaturezaDeLancamentoRepository : INaturezaDeLancamentoRepository
    {

        private readonly AppDbContext _context;

        public NaturezaDeLancamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NaturezaDeLancamento> Adicionar(NaturezaDeLancamento entidade)
        {
            await _context.NaturezaDeLancamentos.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<NaturezaDeLancamento> Atualizar(NaturezaDeLancamento entidade)
        {
            NaturezaDeLancamento? entidadeBanco = await _context.NaturezaDeLancamentos
           .Where(u => u.Id == entidade.Id)
           .FirstOrDefaultAsync();

            if (entidadeBanco != null)
            {
                _context.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
                _context.Update(entidadeBanco);

                await _context.SaveChangesAsync();
                return entidadeBanco;
            }
            else
            {
                throw new NotFoundException("O titulo não foi localizado");
            }
        }

        public async Task Deletar(NaturezaDeLancamento entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }
        public async Task<IEnumerable<NaturezaDeLancamento>> ObterTodos()
        {
            return await _context.NaturezaDeLancamentos.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }


        public async Task<NaturezaDeLancamento?> ObterPorId(long id)
        {
            return await _context.NaturezaDeLancamentos.AsNoTracking()
                                                       .Where(u => u.Id == id)
                                                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> ObeterPeloIdUsuario(long IdUsuario)
        {
            return await _context.NaturezaDeLancamentos.AsNoTracking()
            .Where(n => n.IdUsuario == IdUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }
    }
}