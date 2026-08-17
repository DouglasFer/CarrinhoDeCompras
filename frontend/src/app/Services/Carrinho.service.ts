import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICarrinho } from '../Core/Interface/ICarrinho.interface';

@Injectable({ providedIn: 'root' })
export class CarrinhoServico {
  private readonly apiUrl = 'http://localhost:5282/api/carrinhos';

  constructor(private readonly http: HttpClient) {}

  criar(): Observable<ICarrinho> {
    return this.http.post<ICarrinho>(this.apiUrl, {});
  }

  obterPorId(id: number): Observable<ICarrinho> {
    return this.http.get<ICarrinho>(`${this.apiUrl}/${id}`);
  }

  obterHistorico(): Observable<ICarrinho[]> {
    return this.http.get<ICarrinho[]>(`${this.apiUrl}/historico`);
  }

  adicionarItem(carrinhoId: number, produtoId: number, quantidade: number): Observable<ICarrinho> {
    return this.http.post<ICarrinho>(`${this.apiUrl}/${carrinhoId}/itens`, { produtoId, quantidade });
  }

  alterarQuantidade(carrinhoId: number, produtoId: number, quantidade: number): Observable<ICarrinho> {
    return this.http.put<ICarrinho>(`${this.apiUrl}/${carrinhoId}/itens/${produtoId}`, { quantidade });
  }

  removerItem(carrinhoId: number, produtoId: number): Observable<ICarrinho> {
    return this.http.delete<ICarrinho>(`${this.apiUrl}/${carrinhoId}/itens/${produtoId}`);
  }

  aplicarCupom(carrinhoId: number, codigoCupom: string): Observable<ICarrinho> {
    return this.http.post<ICarrinho>(`${this.apiUrl}/${carrinhoId}/cupom`, { codigoCupom });
  }

  removerCupom(carrinhoId: number): Observable<ICarrinho> {
    return this.http.delete<ICarrinho>(`${this.apiUrl}/${carrinhoId}/cupom`);
  }

  finalizar(carrinhoId: number): Observable<ICarrinho> {
    return this.http.post<ICarrinho>(`${this.apiUrl}/${carrinhoId}/finalizar`, {});
  }
}