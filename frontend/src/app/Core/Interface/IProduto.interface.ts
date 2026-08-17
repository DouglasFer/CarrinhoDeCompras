export interface IProduto {
    id: number;
    descricaoProduto: string;
    precoUnitario: number;
    quantidadeEstoque: number;
  }

export interface IProdutoComQuantidade extends IProduto {
  quantidadeSelecionada: number;
}