import { Routes } from "@angular/router";
import { CriaturaLista } from "./criaturas/criatura-lista";
import { CriaturaCrear } from "./criaturas/criatura-crear";
import { CriaturaBuscar } from "./criaturas/criatura-buscar";

export const routes: Routes = [
  { path: "",                 redirectTo: "criaturas", pathMatch: "full" },
  { path: "criaturas",        component: CriaturaLista },
  { path: "criaturas/nueva",  component: CriaturaCrear },
  { path: "criaturas/buscar", component: CriaturaBuscar }
];
