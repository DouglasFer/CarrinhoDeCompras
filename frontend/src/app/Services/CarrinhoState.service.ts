import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ICarrinho } from '../Core/Interface/ICarrinho.interface';
import { CarrinhoServico } from './Carrinho.service';

@Injectable({ providedIn: 'root' })
export class CarrinhoStateServico {
  private readonly carrinhoSubject = new BehaviorSubject<ICarrinho | null>(null);
  readonly carrinho$: Observable<ICarrinho | null> = this.carrinhoSubject.asObservable();

  private readonly painelAbertoSubject = new BehaviorSubject<boolean>(false);
  readonly painelAberto$: Observable<boolean> = this.painelAbertoSubject.asObservable();

  constructor(private readonly carrinhoServico: CarrinhoServico) {
    this.inicializar();
  }

  get carrinhoId(): number | null {
    return this.carrinhoSubject.value?.id ?? null;
  }

  abrirPainel(): void {
    this.painelAbertoSubject.next(true);
  }

  fecharPainel(): void {
    this.painelAbertoSubject.next(false);
  }

  togglePainel(): void {
    this.painelAbertoSubject.next(!this.painelAbertoSubject.value);
  }

  adicionarItem(produtoId: number, quantidade: number): Observable<ICarrinho> {
    return this.executarEAtualizar(this.carrinhoServico.adicionarItem(this.exigirCarrinhoId(), produtoId, quantidade));
  }

  alterarQuantidade(produtoId: number, quantidade: number): Observable<ICarrinho> {
    return this.executarEAtualizar(this.carrinhoServico.alterarQuantidade(this.exigirCarrinhoId(), produtoId, quantidade));
  }

  removerItem(produtoId: number): Observable<ICarrinho> {
    return this.executarEAtualizar(this.carrinhoServico.removerItem(this.exigirCarrinhoId(), produtoId));
  }

  aplicarCupom(codigoCupom: string): Observable<ICarrinho> {
    return this.executarEAtualizar(this.carrinhoServico.aplicarCupom(this.exigirCarrinhoId(), codigoCupom));
  }

  removerCupom(): Observable<ICarrinho> {
    return this.executarEAtualizar(this.carrinhoServico.removerCupom(this.exigirCarrinhoId()));
  }

  finalizar(): Observable<ICarrinho> {
    return this.executarEAtualizar(this.carrinhoServico.finalizar(this.exigirCarrinhoId()));
  }

  private exigirCarrinhoId(): number {
    const id = this.carrinhoId;
    if (!id) {
      throw new Error('O carrinho ainda está sendo carregado. Aguarde um instante e tente novamente.');
    }
    return id;
  }

  private executarEAtualizar(operacao: Observable<ICarrinho>): Observable<ICarrinho> {
    return operacao.pipe(
      tap((carrinho) => this.carrinhoSubject.next(carrinho))
    );
  }

  private inicializar(): void {
    const idSalvo = localStorage.getItem('carrinhoId');

    if (!idSalvo) {
      this.criarNovoCarrinho();
      return;
    }

    this.carrinhoServico.obterPorId(Number(idSalvo)).subscribe({
      next: (carrinho) => this.tratarCarrinhoEncontrado(carrinho),
      error: () => this.criarNovoCarrinho()
    });
  }

  private tratarCarrinhoEncontrado(carrinho: ICarrinho): void {
    const podeContinuarUsando = carrinho.status === 'Aberto';

    if (podeContinuarUsando) {
      this.carrinhoSubject.next(carrinho);
    } else {
      this.criarNovoCarrinho();
    }
  }

  private criarNovoCarrinho(): void {
    this.carrinhoServico.criar().subscribe((carrinho) => {
      localStorage.setItem('carrinhoId', String(carrinho.id));
      this.carrinhoSubject.next(carrinho);
    });
  }
}