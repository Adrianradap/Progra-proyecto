import { Component, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { ApiClient } from "../core/http/api-client";
import { Criatura } from "./criatura";

@Component({
  selector: "app-criatura-buscar",
  imports: [FormsModule],
  templateUrl: "./criatura-buscar.html",
})
export class CriaturaBuscar {
  private api = inject(ApiClient);
  private url = "http://localhost:5000/api/criaturas";

  buscarId: number = 0;
  criatura: Criatura | null = null;
  mensajeError: string = "";

  buscar(): void {
    this.criatura = null;
    this.mensajeError = "";
    this.api.get<Criatura>(this.url + "/" + this.buscarId).subscribe({
      next: data => (this.criatura = data),
      error: err => (this.mensajeError = err.error?.mensaje ?? "Error al buscar.")
    });
  }
}
