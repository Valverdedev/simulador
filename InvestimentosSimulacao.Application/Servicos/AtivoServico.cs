using AutoMapper;
using FluentValidation;
using InvestimentosSimulacao.Application.Dtos.Ativo;
using InvestimentosSimulacao.Application.Interfaces;
using InvestimentosSimulacao.Application.Interfaces.AplicacaoServicos;
using InvestimentosSimulacao.Domain.Dominio.Entidades;

namespace InvestimentosSimulacao.Application.Servicos;

public class AtivoServico : IAtivoServico
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AtivoInserirDto> _validator;
    private readonly IMapper _mapper;

    public AtivoServico(IUnitOfWork unitOfWork, IValidator<AtivoInserirDto> validador,
                        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _validator = validador;
        _mapper = mapper;
    }

    public Task<AtivoDto> ObterPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AtivoDto>> ObterTodosAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AdicionarAsync(AtivoInserirDto dto)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(dto);
            var ativo = _mapper.Map<Ativo>(dto);
            await _unitOfWork.Ativos.AdicionarAsync(ativo);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
        }
        finally
        {
            await _unitOfWork.CompleteAsync();
        }
       
    }

    public Task AtualizarAsync(int id, AtivoAtualizarDto dto)
    {
        throw new NotImplementedException();
    }

    public Task RemoverAsync(int id)
    {
        throw new NotImplementedException();
    }
}