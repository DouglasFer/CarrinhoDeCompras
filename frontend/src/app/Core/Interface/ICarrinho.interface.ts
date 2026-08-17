import { IItemCarrinho } from "./IItemCarrinho.interface";

export type StatusCarrinho = 'Aberto' | 'Finalizado';

export interface ICarrinho {
  id: number;
  status: StatusCarrinho;
  itens: IItemCarrinho[];
  codigoCupom: string | null;
  subtotal: number;
  valorDesconto: number;
  total: number;
}