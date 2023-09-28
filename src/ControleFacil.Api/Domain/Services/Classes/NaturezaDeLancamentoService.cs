using AutoMapper;
using ControleFacil.Api.Contract.NaturezaDeLancamento;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class NaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long>
    {
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRespository;
        private readonly IMapper _mapper;

        public NaturezaDeLancamentoService(INaturezaDeLancamentoRepository naturezaDeLancamentoRespository,
         IMapper mapper)
        {
            _naturezaDeLancamentoRespository = naturezaDeLancamentoRespository;
            _mapper = mapper;
        }


        public async Task<NaturezaDeLancamentoResponseContract> Adicionar(NaturezaDeLancamentoRequestContract entidade, long idUsuario)
        {
            var naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(entidade);

            naturezaDeLancamento.DataCadastro = DateTime.Now;
            naturezaDeLancamento.IdUsuario = idUsuario;

            naturezaDeLancamento = await _naturezaDeLancamentoRespository.Adicionar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        public async Task<NaturezaDeLancamentoResponseContract> Atualizar(long id, NaturezaDeLancamentoRequestContract entidade, long idUsuario)
        {


            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            naturezaDeLancamento.Descricao = entidade.Descricao;
            naturezaDeLancamento.Observacao = entidade.Observacao;

            naturezaDeLancamento = await _naturezaDeLancamentoRespository.Atualizar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            await _naturezaDeLancamentoRespository.Deletar(naturezaDeLancamento);
        }
        public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> ObterTodos(long idUsuario)
        {
            var naturezaDeLancamento = await _naturezaDeLancamentoRespository.ObeterPeloIdUsuario(idUsuario);
            return naturezaDeLancamento.Select(natureza => _mapper.Map<NaturezaDeLancamentoResponseContract>(natureza));
        }

        public async Task<NaturezaDeLancamentoResponseContract> ObterPorId(long id, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }


        private async Task<NaturezaDeLancamento> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var naturezaDeLancamento = await _naturezaDeLancamentoRespository.ObterPorId(id);
            if (naturezaDeLancamento is null || naturezaDeLancamento.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma natureza de lançamento pelo id {id}");
            }

            return naturezaDeLancamento;
        }
    }
}