import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';
import { Criatura, CrearCriaturaRequest, TIPOS_VALIDOS } from './criatura';

@Component({
  selector: 'app-criatura-crear',
  imports: [RouterLink, FormsModule],
  templateUrl: './criatura-crear.html',
})
export class CriaturaCrear {
  private api = inject(ApiClient);
  private router = inject(Router);
  private url = 'http://localhost:5000/api/criaturas'; // Cambiar si el puerto es diferente

  readonly tiposValidos = TIPOS_VALIDOS;
  mensajeError: string = '';

  criatura: CrearCriaturaRequest = {
    nombre: '',
    tipo: '',
    hp: 0,
    atk: 0,
    def: 0
  };

  guardar(): void {
    this.mensajeError = '';
    this.api.post<Criatura>(this.url, this.criatura).subscribe({
      next: () => this.router.navigate(['/criaturas']),
      error: err => {
        this.mensajeError = err.error?.mensaje ?? 'Error al crear la criatura.';
      }
    });
  }
}
