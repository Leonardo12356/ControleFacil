using AutoMapper;
using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class ApagarService : IService<ApagarRequestContract, ApagarResponseContract, long>
    {
        private readonly IApagarRepository _apagarRespository;
        private readonly IMapper _mapper;

        public ApagarService(IApagarRepository apagarRespository,
         IMapper mapper)
        {
            _apagarRespository = apagarRespository;
            _mapper = mapper;
        }


        public async Task<ApagarResponseContract> Adicionar(ApagarRequestContract entidade, long idUsuario)
        {

            Validar(entidade);
            var Apagar = _mapper.Map<Apagar>(entidade);

            Apagar.DataCadastro = DateTime.Now;
            Apagar.IdUsuario = idUsuario;

            Apagar = await _apagarRespository.Adicionar(Apagar);

            return _mapper.Map<ApagarResponseContract>(Apagar);
        }

        public async Task<ApagarResponseContract> Atualizar(long id, ApagarRequestContract entidade, long idUsuario)
        {
            Validar(entidade);

            Apagar apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map<Apagar>(entidade);
            contrato.IdUsuario = apagar.IdUsuario;
            contrato.Id = apagar.Id;
            contrato.DataCadastro = apagar.DataCadastro;

            contrato = await _apagarRespository.Atualizar(contrato);

            return _mapper.Map<ApagarResponseContract>(contrato);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            await _apagarRespository.Deletar(apagar);
        }
        public async Task<IEnumerable<ApagarResponseContract>> ObterTodos(long idUsuario)
        {
            var titulosApagar = await _apagarRespository.ObeterPeloIdUsuario(idUsuario);
            return titulosApagar.Select(titulo => _mapper.Map<ApagarResponseContract>(titulo));
        }

        public async Task<ApagarResponseContract> ObterPorId(long id, long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<ApagarResponseContract>(apagar);
        }


        private async Task<Apagar> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var apagar = await _apagarRespository.ObterPorId(id);
            if (apagar is null || apagar.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhum titutlo apagar pelo id {id}");
            }

            return apagar;
        }

        private static void Validar(ApagarRequestContract entidade)
        {
            if (entidade.ValorOriginal < 0 || entidade.ValorPago < 0)
            {
                throw new BadRequestException("Os campos ValorOriginal e ValorPago não podem ser negativos.");
            }
        }
    }
}