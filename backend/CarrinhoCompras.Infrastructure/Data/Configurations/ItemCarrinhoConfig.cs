using CarrinhoCompras.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoCompras.Infrastructure.Data.Configurations;

public class ItemCarrinhoConfiguration : IEntityTypeConfiguration<ItemCarrinho>
{
    public void Configure(EntityTypeBuilder<ItemCarrinho> builder)
    {
        builder.ToTable("ItensCarrinho");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantidade).IsRequired();

        builder.Property(i => i.PrecoUnitario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Ignore(i => i.PrecoItem);

        builder.HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}