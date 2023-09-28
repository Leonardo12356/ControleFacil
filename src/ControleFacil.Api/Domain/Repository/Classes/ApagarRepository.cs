using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{
    public class ApagarRepository : IApagarRepository
    {

        private readonly AppDbContext _context;

        public ApagarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Apagar> Adicionar(Apagar entidade)
        {
            await _context.Apagar.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Apagar> Atualizar(Apagar entidade)
        {


            Apagar? entidadeBanco = await _context.Apagar
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


        public async Task Deletar(Apagar entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }
        public async Task<IEnumerable<Apagar>> ObterTodos()
        {
            return await _context.Apagar.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }


        public async Task<Apagar?> ObterPorId(long id)
        {
            return await _context.Apagar.AsNoTracking()
                                                       .Where(u => u.Id == id)
                                                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Apagar>> ObeterPeloIdUsuario(long IdUsuario)
        {
            return await _context.Apagar.AsNoTracking()
            .Where(n => n.IdUsuario == IdUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }
    }
}