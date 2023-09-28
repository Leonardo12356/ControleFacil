using AutoMapper;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class AreceberService : IService<AreceberRequestContract, AreceberResponseContract, long>
    {
        private readonly IAreceberRepository _areceberRespository;
        private readonly IMapper _mapper;

        public AreceberService(IAreceberRepository areceberRespository,
         IMapper mapper)
        {
            _areceberRespository = areceberRespository;
            _mapper = mapper;
        }


        public async Task<AreceberResponseContract> Adicionar(AreceberRequestContract entidade, long idUsuario)
        {
            Validar(entidade);

            var areceber = _mapper.Map<Areceber>(entidade);

            areceber.DataCadastro = DateTime.Now;
            areceber.IdUsuario = idUsuario;

            areceber = await _areceberRespository.Adicionar(areceber);

            return _mapper.Map<AreceberResponseContract>(areceber);
        }

        public async Task<AreceberResponseContract> Atualizar(long id, AreceberRequestContract entidade, long idUsuario)
        {
            Validar(entidade);

            Areceber areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map<Areceber>(entidade);
            contrato.IdUsuario = areceber.IdUsuario;
            contrato.Id = areceber.Id;
            contrato.DataCadastro = areceber.DataCadastro;

            contrato = await _areceberRespository.Atualizar(contrato);

            return _mapper.Map<AreceberResponseContract>(contrato);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            await _areceberRespository.Deletar(areceber);
        }
        public async Task<IEnumerable<AreceberResponseContract>> ObterTodos(long idUsuario)
        {
            var titulosAreceber = await _areceberRespository.ObeterPeloIdUsuario(idUsuario);
            return titulosAreceber.Select(titulo => _mapper.Map<AreceberResponseContract>(titulo));
        }

        public async Task<AreceberResponseContract> ObterPorId(long id, long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<AreceberResponseContract>(areceber);
        }


        private async Task<Areceber> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var areceber = await _areceberRespository.ObterPorId(id);
            if (areceber is null || areceber.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhum titulo Areceber pelo id {id}");
            }

            return areceber;
        }

        private static void Validar(AreceberRequestContract entidade)
        {
            if (entidade.ValorOriginal < 0 || entidade.ValorRecebido < 0)
            {
                throw new BadRequestException("Os campos ValorOriginal e ValorRecebido não podem ser negativos.");
            }
        }
    }
}