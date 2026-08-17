using CarrinhoCompras.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoCompras.Infrastructure.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("ID")
            .ValueGeneratedNever();

        builder.Property(p => p.DescricaoProduto)
            .HasColumnName("DescricaoProduto")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.PrecoUnitario)
            .HasColumnName("PrecoUnitario")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.QuantidadeEstoque)
            .HasColumnName("QuantidadeEstoque")
            .IsRequired();

        builder.HasData(
            new { Id = 1, DescricaoProduto = "Mouse sem fio", PrecoUnitario = 79.90m, QuantidadeEstoque = 50 },
            new { Id = 2, DescricaoProduto = "Teclado mecânico", PrecoUnitario = 249.90m, QuantidadeEstoque = 30 },
            new { Id = 3, DescricaoProduto = "Monitor 24\" Full HD", PrecoUnitario = 799.90m, QuantidadeEstoque = 15 },
            new { Id = 4, DescricaoProduto = "Notebook 15\"", PrecoUnitario = 3499.90m, QuantidadeEstoque = 8 },
            new { Id = 5, DescricaoProduto = "Fone de ouvido Bluetooth", PrecoUnitario = 199.90m, QuantidadeEstoque = 40 },
            new { Id = 6, DescricaoProduto = "Webcam Full HD", PrecoUnitario = 149.90m, QuantidadeEstoque = 25 },
            new { Id = 7, DescricaoProduto = "Cadeira de escritório", PrecoUnitario = 899.90m, QuantidadeEstoque = 12 },
            new { Id = 8, DescricaoProduto = "SSD 480GB", PrecoUnitario = 249.90m, QuantidadeEstoque = 20 },
            new { Id = 9, DescricaoProduto = "Carregador USB-C 65W", PrecoUnitario = 89.90m, QuantidadeEstoque = 60 },
            new { Id = 10, DescricaoProduto = "Mochila para notebook", PrecoUnitario = 129.90m, QuantidadeEstoque = 35 }
        );
    }
}