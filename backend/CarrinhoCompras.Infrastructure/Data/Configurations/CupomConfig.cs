using CarrinhoCompras.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoCompras.Infrastructure.Data.Configurations;

public class CupomConfiguration : IEntityTypeConfiguration<Cupom>
{
    public void Configure(EntityTypeBuilder<Cupom> builder)
    {
        builder.ToTable("Cupons");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("ID")
            .ValueGeneratedNever();

        builder.Property(c => c.CodigoCupom)
            .HasColumnName("CodigoCupom")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.DataExpiracao)
            .HasColumnName("DataValidade")
            .IsRequired();

        builder.HasIndex(c => c.CodigoCupom)
            .IsUnique();

        builder.Property(c => c.PercentualDesconto)
            .HasColumnName("PercentualDesconto")
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.HasData(
            new { Id = 1, CodigoCupom = "10OFF", PercentualDesconto = 10m, DataExpiracao = new DateTime(2026, 9, 15, 0, 0, 0, DateTimeKind.Utc)  },
            new { Id = 2, CodigoCupom = "15OFF", PercentualDesconto = 15m, DataExpiracao = new DateTime(2026, 9, 15, 0, 0, 0, DateTimeKind.Utc)  },
            new { Id = 3, CodigoCupom = "20OFF", PercentualDesconto = 20m, DataExpiracao = new DateTime(2026, 9, 15, 0, 0, 0, DateTimeKind.Utc)  }
        );
    }
}