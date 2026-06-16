// Modelo principal - espeja la clase Criatura del backend
export interface Criatura {
  id: number;
  nombre: string;
  tipo: string;
  hp: number;
  atk: number;
  def: number;
}

// DTO para crear una nueva criatura
export interface CrearCriaturaRequest {
  nombre: string;
  tipo: string;
  hp: number;
  atk: number;
  def: number;
}

// DTO para simular un ataque
export interface AtaqueRequest {
  atacanteId: number;
  objetivoId: number;
}

// Respuesta del endpoint atacar
export interface AtaqueResultado {
  mensaje: string;
  atacante: string;
  objetivo: string;
  danio: number;
  hpRestante: number;
  derrotado: boolean;
}

// Tipos validos del juego
export const TIPOS_VALIDOS = ['Fuego', 'Agua', 'Planta', 'Roca', 'Electrico'] as const;
