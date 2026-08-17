using CarrinhoCompras.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoCompras.Infrastructure.Data.Configurations;

public class CarrinhoConfiguration : IEntityTypeConfiguration<Carrinho>
{
    public void Configure(EntityTypeBuilder<Carrinho> builder)
    {
        builder.ToTable("Carrinhos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.CriadoEm).IsRequired();
        builder.Property(c => c.AtualizadoEm).IsRequired();

        builder.HasOne(c => c.CupomAplicado)
            .WithMany()
            .HasForeignKey(c => c.CupomAplicadoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Itens)
            .WithOne()
            .HasForeignKey(i => i.CarrinhoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Itens)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}