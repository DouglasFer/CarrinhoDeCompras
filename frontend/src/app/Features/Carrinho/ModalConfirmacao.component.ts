import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export interface ConfirmacaoDialogData {
  titulo: string;
  mensagem: string;
  textoConfirmar?: string;
  textoCancelar?: string;
}

@Component({
  selector: 'app-confirmacao-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule],
  template: `
    <div class="confirmacao-dialog">
      <div class="confirmacao-icone">
        <mat-icon>delete_outline</mat-icon>
      </div>

      <h2 mat-dialog-title>{{ data.titulo }}</h2>

      <mat-dialog-content>
        <p>{{ data.mensagem }}</p>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-stroked-button (click)="cancelar()">
          {{ data.textoCancelar || 'Cancelar' }}
        </button>

        <button mat-raised-button color="warn" (click)="confirmar()">
          {{ data.textoConfirmar || 'Remover' }}
        </button>
      </mat-dialog-actions>
    </div>
  `,
  styles: [`
    .confirmacao-dialog {
      padding: 8px;
    }

    .confirmacao-icone {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 56px;
      height: 56px;
      margin: 0 auto 12px;
      border-radius: 50%;
      background: #fee2e2;
    }

    .confirmacao-icone mat-icon {
      color: #d32f2f;
      font-size: 28px;
      width: 28px;
      height: 28px;
    }

    h2[mat-dialog-title] {
      margin: 0 0 8px;
      font-size: 1.15rem;
      color: #1f2937;
      text-align: center;
    }

    mat-dialog-content p {
      margin: 0;
      color: #6b7280;
      font-size: 0.9rem;
      line-height: 1.5;
    }

    mat-dialog-actions {
      margin-top: 20px;
      padding: 0;
    }
  `]
})
export class ConfirmacaoDialogComponent {
  constructor(
    private readonly dialogRef: MatDialogRef<ConfirmacaoDialogComponent, boolean>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmacaoDialogData
  ) {}

  confirmar(): void {
    this.dialogRef.close(true);
  }

  cancelar(): void {
    this.dialogRef.close(false);
  }
}