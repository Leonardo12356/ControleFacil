using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{
    public class AreceberRepository : IAreceberRepository
    {

        private readonly AppDbContext _context;

        public AreceberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Areceber> Adicionar(Areceber entidade)
        {
            await _context.Areceber.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Areceber> Atualizar(Areceber entidade)
        {
            Areceber? entidadeBanco = await _context.Areceber
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

        public async Task Deletar(Areceber entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }
        public async Task<IEnumerable<Areceber>> ObterTodos()
        {
            return await _context.Areceber.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }


        public async Task<Areceber?> ObterPorId(long id)
        {
            return await _context.Areceber.AsNoTracking()
                                                       .Where(u => u.Id == id)
                                                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Areceber>> ObeterPeloIdUsuario(long IdUsuario)
        {
            return await _context.Areceber.AsNoTracking()
            .Where(n => n.IdUsuario == IdUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }
    }
}