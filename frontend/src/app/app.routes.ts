import { Routes } from '@angular/router';
import { LojaLayoutComponent } from './Features/LojaLayout/LojaLayout.component';

export const routes: Routes = [
  { path: '', redirectTo: 'catalogo', pathMatch: 'full' },
  { path: 'catalogo', component: LojaLayoutComponent },
];