using AutoMapper;
using ControleFacil.Api.Contract.NaturezaDeLancamento;
using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.AutoMapper
{
    public class NaturezaDeLancamentoProfile : Profile
    {
        public NaturezaDeLancamentoProfile()
        {
            CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoRequestContract>().ReverseMap();
            CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoResponseContract>().ReverseMap();
        }
    }
}