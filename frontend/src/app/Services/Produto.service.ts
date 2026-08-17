import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IProduto } from '../Core/Interface/IProduto.interface';

@Injectable({ providedIn: 'root' })
export class ProdutoServico {
  private readonly apiUrl = 'http://localhost:5282/api/produtos'; 

  constructor(private readonly http: HttpClient) {}

  listar(): Observable<IProduto[]> {
    return this.http.get<IProduto[]>(this.apiUrl);
  }

  obterPorId(id: number): Observable<IProduto> {
    return this.http.get<IProduto>(`${this.apiUrl}/${id}`);
  }
}