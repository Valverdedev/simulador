using InvestimentosSimulacao.Domain.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestimentosSimulacao.Infrastructure.Postgre.MapeamentosEntidades;

public class AtivoMap : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        builder.ToTable("ativos");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.TipoInvestimento)
            .HasColumnName("tipo_investimento")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.OwnsOne(x => x.TaxaDeRetorno, tr =>
        {
            tr.Property(x => x.Valor)
                .HasColumnName("taxa_retorno")
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
        });
    }
    
}