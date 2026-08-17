import { CommonModule } from "@angular/common";
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Subscription } from "rxjs";
import { IProdutoComQuantidade } from "../../Core/Interface/IProduto.interface";
import { CarrinhoStateServico } from "../../Services/CarrinhoState.service";
import { ProdutoServico } from "../../Services/Produto.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IItemCarrinho } from "../../Core/Interface/IItemCarrinho.interface";


@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatBadgeModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './Catalogo.component.html',
  styleUrl: './Catalogo.component.css'
})
export class CatalogoComponent implements OnInit, OnDestroy {
  produtos: IProdutoComQuantidade[] = [];
  carregando = true;
  totalItens = 0;

  private carrinhoSub?: Subscription;

  constructor(
    private readonly produtoServico: ProdutoServico,
    protected readonly carrinhoState: CarrinhoStateServico,
    private readonly snackBar: MatSnackBar,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.produtoServico.listar().subscribe({
      next: (produtos) => {
        this.produtos = produtos.map(p => ({
          ...p,
          quantidadeSelecionada: 1
        }));

        this.carregando = false;

        this.cdr.detectChanges();
      },

      error: () => {
        this.snackBar.open(
          'Erro ao carregar produtos.',
          'Fechar',
          { duration: 3000 }
        );

        this.carregando = false;

        this.cdr.detectChanges();
      }
    });

    this.carrinhoSub = this.carrinhoState.carrinho$.subscribe((carrinho) => {
      this.totalItens = carrinho?.itens?.length ?? 0;
      this.cdr.detectChanges();
    });
  }

  ngOnDestroy(): void {
    this.carrinhoSub?.unsubscribe();
  }

  adicionarAoCarrinho(produto: IProdutoComQuantidade): void {
    this.carrinhoState.adicionarItem(produto.id, produto.quantidadeSelecionada).subscribe({
      next: () => {
        this.snackBar.open(`${produto.descricaoProduto} adicionado ao carrinho.`, 'Fechar', { duration: 2000 });
        produto.quantidadeSelecionada = 1;
        this.carrinhoState.abrirPainel();
      },
      error: (erro) => {
        const mensagem = erro?.error?.detail ?? 'Não foi possível adicionar o produto.';
        this.snackBar.open(mensagem, 'Fechar', { duration: 3000 });
      }
    });
  }

  abrirCarrinho(): void {
    this.carrinhoState.abrirPainel();
  }

  limitarQuantidade(item: IProdutoComQuantidade, event: Event): void {
    const input = event.target as HTMLInputElement;
  
    let quantidade = Number(input.value);
  
    if (!quantidade || quantidade < 1) {
      quantidade = 1;
    }
  
    quantidade = Math.min(
      quantidade,
      999,
      item.quantidadeEstoque
    );
  
    item.quantidadeSelecionada = quantidade;
    input.value = quantidade.toString();
  }
}