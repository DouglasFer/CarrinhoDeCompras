using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarrinhoCompras.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tabelasIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    CodigoCupom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PercentualDesconto = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    DescricaoProduto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CupomAplicadoId = table.Column<int>(type: "integer", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Cupons_CupomAplicadoId",
                        column: x => x.CupomAplicadoId,
                        principalTable: "Cupons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItensCarrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarrinhoId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCarrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCarrinho_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCarrinho_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cupons",
                columns: new[] { "ID", "CodigoCupom", "DataValidade", "PercentualDesconto" },
                values: new object[,]
                {
                    { 1, "10OFF", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 10m },
                    { 2, "15OFF", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 15m },
                    { 3, "20OFF", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 20m }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ID", "DescricaoProduto", "PrecoUnitario", "QuantidadeEstoque" },
                values: new object[,]
                {
                    { 1, "Mouse sem fio", 79.90m, 50 },
                    { 2, "Teclado mecânico", 249.90m, 30 },
                    { 3, "Monitor 24\" Full HD", 799.90m, 15 },
                    { 4, "Notebook 15\"", 3499.90m, 8 },
                    { 5, "Fone de ouvido Bluetooth", 199.90m, 40 },
                    { 6, "Webcam Full HD", 149.90m, 25 },
                    { 7, "Cadeira de escritório", 899.90m, 12 },
                    { 8, "SSD 480GB", 249.90m, 20 },
                    { 9, "Carregador USB-C 65W", 89.90m, 60 },
                    { 10, "Mochila para notebook", 129.90m, 35 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_CupomAplicadoId",
                table: "Carrinhos",
                column: "CupomAplicadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_CodigoCupom",
                table: "Cupons",
                column: "CodigoCupom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_CarrinhoId",
                table: "ItensCarrinho",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_ProdutoId",
                table: "ItensCarrinho",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensCarrinho");

            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Cupons");
        }
    }
}
