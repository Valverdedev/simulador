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
using InvestimentosSimulacao.Application.Validacoes;
using InvestimentosSimulacao.Domain.Dominio.Entidades;
using InvestimentosSimulacao.Domain.Enums;
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
    public async Task AdicionarAsync_DeveRetornarErroQuandoFalharValidacao()
    {
        var dto = new AtivoInserirDto
        {
            Nome = "A",
            TipoInvestimento = InvestimentosSimulacao.Domain.Enums.TipoInvestimentoEnum.cdb,
            TaxaDeRetorno = 5.5
        };
        var validador = new AtivoInseriValidar();
        var resultado = await validador.ValidateAsync(dto);
        Assert.False(resultado.IsValid);
       
    }
    
    [Fact]
    public async Task AdicionarAsync_DeveRetornarSucessoQuandoValidarSucesso ()
    {
        var dto = new AtivoInserirDto
        {
            Nome = "ABC", // Nome inválido (muito curto)
            TipoInvestimento = InvestimentosSimulacao.Domain.Enums.TipoInvestimentoEnum.cdb,
            TaxaDeRetorno = 5.5
        };

        var validador = new AtivoInseriValidar();
        var resultado = await validador.ValidateAsync(dto);
        Assert.True(resultado.IsValid);
    }
    
    [Fact]
    public void AdicionarAtivo_DeveLancarErroQuandoTaxaDeRetornoForNegativa()
    {
        var excecao = Assert.Throws<ArgumentException>(() =>
            new TaxaDeRetorno(-0.05));

        Assert.Equal("A taxa de retorno deve ser maior ou igual a zero.", excecao.Message);
    }
    
}