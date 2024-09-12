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

    public async Task<AtivoDto> ObterPorIdAsync(int id)
    {
        try
        {
            var ativo =  await _unitOfWork.Ativos.ObterPorIdAsync(id);
            return _mapper.Map<AtivoDto>(ativo);
 
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao obter ativo por id", e);
        }
        
    }

    public async Task<IEnumerable<AtivoDto>> ObterTodosAsync()
    {
        try
        {
            var ativos = await _unitOfWork.Ativos.ListarTodosAsync();
            return _mapper.Map<IEnumerable<AtivoDto>>(ativos);
        }
        catch (Exception e)   
        {
            throw new Exception("Erro ao obter todos os ativos", e);
        }
    }

    public async Task AdicionarAsync(AtivoInserirDto dto)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(dto);

            var ativo = _mapper.Map<Ativo>(dto);
            await _unitOfWork.Ativos.AdicionarAsync(ativo);

            await _unitOfWork.CompleteAsync();
        }
        catch (ValidationException e)
        {
            await _unitOfWork.RollbackAsync();
            throw;
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