using FluentValidation;
using InvestimentosSimulacao.Application.Dtos.Ativo;

namespace InvestimentosSimulacao.Application.Validacoes;

public class AtivoInseriValidar : AbstractValidator<AtivoInserirDto>
{
    public AtivoInseriValidar()
    {
        
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        
        
        RuleFor(x => x.TipoInvestimento)
            .IsInEnum().WithMessage("O tipo de investimento é inválido.");

        
        RuleFor(x => x.TaxaDeRetorno)
            .NotNull().WithMessage("A taxa de retorno é obrigatória.")
            .GreaterThan(0).WithMessage("A taxa de retorno deve ser maior que zero.");
    }
    
}