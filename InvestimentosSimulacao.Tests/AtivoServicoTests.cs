using Moq;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using InvestimentosSimulacao.Application.Servicos;
using InvestimentosSimulacao.Application.Interfaces;
using InvestimentosSimulacao.Application.Dtos.Ativo;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using InvestimentosSimulacao.Domain.Dominio.Entidades;
using InvestimentosSimulacao.Domain.ValueObjects;

namespace InvestimentosSimulacao.Tests;

public class AtivoServicoTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AtivoServico _ativoServico;
    private readonly Mock<IValidator<AtivoInserirDto>> _validatorMock;
    private readonly Mock<IAtivoRepositorio> _ativoRepositorioMock;
    
    
    public AtivoServicoTests()
    {
        
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<AtivoInserirDto>>();
        _ativoRepositorioMock = new Mock<IAtivoRepositorio>();
        _ativoServico = new AtivoServico(_unitOfWorkMock.Object, _validatorMock.Object, _mapperMock.Object);
        _unitOfWorkMock.Setup(u => u.Ativos).Returns(_ativoRepositorioMock.Object);

     
        _ativoServico = new AtivoServico(_unitOfWorkMock.Object, _validatorMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task AdicionarAsync_DeveAdicionarAtivoComSucesso()
    {
        
        var dto = new AtivoInserirDto
        {
            Nome = "Ativo Teste",
            TipoInvestimento = InvestimentosSimulacao.Domain.Enums.TipoInvestimentoEnum.cdb,
            TaxaDeRetorno = 5.5
        };

        var ativoMapeado = new Ativo(dto.Nome, dto.TipoInvestimento, new TaxaDeRetorno(dto.TaxaDeRetorno));
        
        _validatorMock.Setup(v => v.ValidateAsync(dto, default))
            .ReturnsAsync(new ValidationResult());
        
        _mapperMock.Setup(m => m.Map<Ativo>(dto)).Returns(ativoMapeado);
        
        await _ativoServico.AdicionarAsync(dto);
        
        _unitOfWorkMock.Verify(u => u.Ativos.AdicionarAsync(It.IsAny<Ativo>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }
    
    [Fact]
    public async Task AdicionarAsync_DeveChamarRollbackEmCasoDeErro()
    {
        
        var dto = new AtivoInserirDto
        {
            Nome = "Ativo Teste",
            TipoInvestimento = InvestimentosSimulacao.Domain.Enums.TipoInvestimentoEnum.cdb,
            TaxaDeRetorno = 5.5
        };

        
        _validatorMock.Setup(v => v.ValidateAsync(dto, default))
            .ReturnsAsync(new ValidationResult());

       
        _mapperMock.Setup(m => m.Map<Ativo>(dto)).Throws(new Exception("Erro de mapeamento"));

        
        await Assert.ThrowsAsync<Exception>(() => _ativoServico.AdicionarAsync(dto));

        _unitOfWorkMock.Verify(u => u.RollbackAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }


    
}