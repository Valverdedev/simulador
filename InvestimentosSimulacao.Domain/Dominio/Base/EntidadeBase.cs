using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestimentosSimulacao.Domain.Dominio.Base
{
    public abstract class EntidadeBase
    {
        [Key]
        public int Id { get; protected set; }
        [Required]
        public DateTime DataCriacao { get; protected set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; protected set; }
        [Required]
        public bool Ativo { get; protected set; } = true;
    
        private readonly List<EventoDominio> _eventosDominio = new List<EventoDominio>();
        public IReadOnlyCollection<EventoDominio> EventosDominio => _eventosDominio.AsReadOnly();

        
        protected EntidadeBase() { }
        
        protected void AdicionarEventoDominio(EventoDominio eventoItem)
        {
            _eventosDominio.Add(eventoItem);
        }
        
        public void RemoverEventoDominio(EventoDominio eventoItem)
        {
            _eventosDominio.Remove(eventoItem);
        }
        
        protected void LimparEventosDominio()
        {
            _eventosDominio.Clear();
        }
        
        
        public void MarcarAtualizacao()
        {
            DataAtualizacao = DateTime.UtcNow;
        }
        
        public void Desativar()
        {
            Ativo = false;
        }
    }
}