import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CarrinhoStateServico } from '../../Services/CarrinhoState.service';
import { CarrinhoComponent } from '../Carrinho/Carrinho.component';
import { CatalogoComponent } from '../Catalogo/catalogo.component';

@Component({
  selector: 'app-loja-layout',
  standalone: true,
  imports: [CommonModule, MatSidenavModule, CatalogoComponent, CarrinhoComponent],
  templateUrl: './LojaLayout.component.html',
  styleUrl: './LojaLayout.component.css'
})
export class LojaLayoutComponent {
  constructor(protected readonly carrinhoState: CarrinhoStateServico) {}
}