import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatExpansionModule } from '@angular/material/expansion';
import { ICarrinho } from '../../../Core/Interface/ICarrinho.interface';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CarrinhoServico } from '../../../Services/Carrinho.service';

@Component({
  selector: 'app-historico',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatExpansionModule
  ],
  templateUrl: './Historico.component.html',
  styleUrl: '../Carrinho.component.css'
})
export class HistoricoComponent implements OnInit {
  pedidos: ICarrinho[] = [];
  carregando = true;

  constructor(
    private readonly carrinhoServico: CarrinhoServico,
    private readonly snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.carrinhoServico.obterHistorico().subscribe({
      next: (pedidos) => {
        this.pedidos = pedidos;
        this.carregando = false;
      },
      error: () => {
        this.snackBar.open('Erro ao carregar histórico de pedidos.', 'Fechar', { duration: 3000 });
        this.carregando = false;
      }
    });
  }

  trackByPedidoId(_: number, pedido: ICarrinho): number {
    return pedido.id;
  }
}