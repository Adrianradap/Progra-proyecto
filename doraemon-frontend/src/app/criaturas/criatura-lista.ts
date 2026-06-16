import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiClient } from '../core/http/api-client';
import { Criatura, AtaqueRequest, AtaqueResultado } from './criatura';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-criatura-lista',
  imports: [RouterLink, FormsModule],
  templateUrl: './criatura-lista.html',
})
export class CriaturaLista {
  private api = inject(ApiClient);
  private url = 'http://localhost:5000/api/criaturas';

  // HU-01: lista de criaturas
  criaturas: Criatura[] = [];

  // HU-05 / HU-06: simular ataque
  atacanteId: number = 0;
  objetivoId: number = 0;
  resultadoAtaque: AtaqueResultado | null = null;
  mensajeAtaqueError: string = '';

  // HU-07: eliminar
  mensajeEliminarError: string = '';

  ngOnInit() {
    this.cargarCriaturas(); // HU-01: carga automatica al abrir
  }

  // HU-01 / HU-08: carga y refresca la lista
  cargarCriaturas() {
    this.api.get<Criatura[]>(this.url).subscribe({
      next: data => this.criaturas = data,
      error: error => console.error('Error al obtener criaturas', error)
    });
  }

  // HU-07: eliminar criatura por ID
  eliminar(id: number): void {
    this.mensajeEliminarError = '';
    this.api.delete(this.url + '/' + id).subscribe({
      next: () => this.cargarCriaturas(), // HU-08: refresca lista
      error: err => this.mensajeEliminarError = err.error?.mensaje ?? 'Error al eliminar.'
    });
  }

  // HU-05 / HU-06: simular ataque con ventaja de tipo
  simularAtaque(): void {
    this.resultadoAtaque = null;
    this.mensajeAtaqueError = '';

    const request: AtaqueRequest = {
      atacanteId: this.atacanteId,
      objetivoId: this.objetivoId
    };

    this.api.post<AtaqueResultado>(this.url + '/atacar', request).subscribe({
      next: resultado => {
        this.resultadoAtaque = resultado;
        this.cargarCriaturas(); // HU-08: refresca HP en la tabla
      },
      error: err => {
        this.mensajeAtaqueError = err.error?.mensaje ?? 'Error al simular ataque.';
      }
    });
  }
}
