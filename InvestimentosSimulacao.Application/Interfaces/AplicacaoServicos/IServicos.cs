namespace InvestimentosSimulacao.Application.Interfaces.AplicacaoServicos;

public interface IServico<TDto, in TCreateDto, in TUpdateDto>
{
    Task<TDto> ObterPorIdAsync(int id);
    Task<IEnumerable<TDto>> ObterTodosAsync();
    Task AdicionarAsync(TCreateDto dto);
    Task AtualizarAsync(int id, TUpdateDto dto);
    Task RemoverAsync(int id);
    
}