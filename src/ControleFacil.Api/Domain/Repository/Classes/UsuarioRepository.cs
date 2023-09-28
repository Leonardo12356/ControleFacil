using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Adicionar(Usuario entidade)
        {
            await _context.Usuarios.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Usuario> Atualizar(Usuario entidade)
        {
            Usuario? entidadeBanco = await _context.Usuarios
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

        public async Task Deletar(Usuario entidade)
        {

            _context.Entry(entidade).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> Obter(string email)
        {
            return await _context.Usuarios.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _context.Usuarios.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }
        public async Task<Usuario?> ObterPorId(long id)
        {
            return await _context.Usuarios.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        }
    }
}