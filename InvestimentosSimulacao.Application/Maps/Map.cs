using AutoMapper;
using InvestimentosSimulacao.Application.Dtos.Ativo;
using InvestimentosSimulacao.Domain.Dominio.Entidades;
using InvestimentosSimulacao.Domain.ValueObjects;

namespace InvestimentosSimulacao.Application.Maps;

public class Map : Profile
{
    
    public Map()
    {
        CreateMap<Ativo, AtivoInserirDto>()
            .ForMember(dest => dest.TaxaDeRetorno, opt => opt.MapFrom(src => src.TaxaDeRetorno.Valor))
            .ReverseMap()
            .ForMember(dest => dest.TaxaDeRetorno, opt => opt.MapFrom(src => new TaxaDeRetorno((double)src.TaxaDeRetorno)));
        
        CreateMap<Ativo, AtivoDto>()
            .ForMember(dest => dest.TaxaDeRetorno, opt => opt.MapFrom(src => src.TaxaDeRetorno.Valor))
            .ReverseMap()
            .ForMember(dest => dest.TaxaDeRetorno, opt => opt.MapFrom(src => new TaxaDeRetorno((double)src.TaxaDeRetorno)));
    }
    
}