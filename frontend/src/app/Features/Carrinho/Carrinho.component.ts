import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CarrinhoStateServico } from '../../Services/CarrinhoState.service';
import { ICarrinho } from '../../Core/Interface/ICarrinho.interface';
import { ConfirmacaoDialogComponent } from './ModalConfirmacao.component';
import { IItemCarrinho } from '../../Core/Interface/IItemCarrinho.interface';
import { Observable } from 'rxjs';
import { CarrinhoServico } from '../../Services/Carrinho.service';
import { IProdutoComQuantidade } from '../../Core/Interface/IProduto.interface';

@Component({
  selector: 'app-carrinho',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatProgressSpinnerModule,
    MatDialogModule
  ],
  templateUrl: './Carrinho.component.html',
  styleUrl: './Carrinho.component.css'
})
export class CarrinhoComponent implements OnInit {
  readonly carrinho$: Observable<ICarrinho | null>;
  codigoCupom = '';

  pedidos: ICarrinho[] = [];
  carregandoHistorico = true;
  historicoCarregado = false;

  constructor(
    private readonly carrinhoState: CarrinhoStateServico,
    private readonly carrinhoServico: CarrinhoServico,
    private readonly snackBar: MatSnackBar,
    private readonly dialog: MatDialog
  ) {
    this.carrinho$ = this.carrinhoState.carrinho$;
  }

  ngOnInit(): void {}

  onTabChange(index: number): void {
    if (index === 1 && !this.historicoCarregado) {
      this.carregarHistorico();
    }
  }

  private carregarHistorico(): void {
    this.carregandoHistorico = true;

    this.carrinhoServico.obterHistorico().subscribe({
      next: (pedidos) => {
        this.pedidos = pedidos;
        this.carregandoHistorico = false;
        this.historicoCarregado = true;
      },
      error: (erro) => {
        this.mostrarErro(this.extrairMensagem(erro));
        this.carregandoHistorico = false;
      }
    });
  }

  trackByPedidoId(_: number, pedido: ICarrinho): number {
    return pedido.id;
  }

  carrinhoAberto(carrinho: ICarrinho): boolean {
    return carrinho.status === 'Aberto';
  }

  alterarQuantidade(produtoId: number, novaQuantidade: number): void {
    if (novaQuantidade <= 0) {
      this.mostrarErro('A quantidade deve ser maior que zero.');
      return;
    }

    this.carrinhoState.alterarQuantidade(produtoId, novaQuantidade).subscribe({
      error: (erro) => this.mostrarErro(this.extrairMensagem(erro))
    });
  }

  confirmarRemocao(item: IItemCarrinho): void {
    const dialogRef = this.dialog.open(ConfirmacaoDialogComponent, {
      width: '360px',
      data: {
        titulo: 'Remover produto',
        mensagem: `Deseja remover "${item.descricaoProduto}" do carrinho? O item será removido por completo, independente da quantidade.`,
        textoConfirmar: 'Remover',
        textoCancelar: 'Cancelar'
      }
    });

    dialogRef.afterClosed().subscribe((confirmado) => {
      if (confirmado) {
        this.removerItem(item.produtoId);
      }
    });
  }

  removerItem(produtoId: number): void {
    this.carrinhoState.removerItem(produtoId).subscribe({
      error: (erro) => this.mostrarErro(this.extrairMensagem(erro))
    });
  }

  aplicarCupom(): void {
    if (!this.codigoCupom.trim()) {
      return;
    }

    this.carrinhoState.aplicarCupom(this.codigoCupom.trim()).subscribe({
      next: () => {
        this.snackBar.open('Cupom aplicado.', 'Fechar', { duration: 2000 });
        this.codigoCupom = '';
      },
      error: (erro) => this.mostrarErro(this.extrairMensagem(erro))
    });
  }

  removerCupom(): void {
    this.carrinhoState.removerCupom().subscribe({
      error: (erro) => this.mostrarErro(this.extrairMensagem(erro))
    });
  }

  finalizar(): void {
    this.carrinhoState.finalizar().subscribe({
      next: () => {
        this.snackBar.open('Compra finalizada com sucesso!', 'Fechar', { duration: 3000 });
        this.historicoCarregado = false;
      },
      error: (erro) => this.mostrarErro(this.extrairMensagem(erro))
    });
  }

  trackByProdutoId(_: number, item: IItemCarrinho): number {
    return item.produtoId;
  }

  private extrairMensagem(erro: any): string {
    return erro?.error?.detail ?? 'Ocorreu um erro inesperado.';
  }

  private mostrarErro(mensagem: string): void {
    this.snackBar.open(mensagem, 'Fechar', { duration: 3000 });
  }

  limitarQuantidade(item: IItemCarrinho): void {
    let quantidade = Number(item.quantidade);

    if (!quantidade || quantidade < 1) {
      quantidade = 1;
    }

    item.quantidade = Math.min(quantidade, 999, item.quantidadeEstoque);
  }
}