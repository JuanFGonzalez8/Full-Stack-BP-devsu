import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface Movimiento {
  movimientoId: string;
  fecha: string;
  tipoMovimiento: string; // "Credito" | "Debito"
  valor: number;
  saldoDisponiblePost: number;
  cuentaId: string;
}

@Injectable({
  providedIn: 'root'
})
export class MovimientosService {
  private api = `${environment.apiUrl}/movimientos`;

  constructor(private http: HttpClient) {}

  // 🔹 Listar movimientos (con filtros opcionales)
  getAll(cuentaId?: string, desde?: string, hasta?: string): Observable<Movimiento[]> {
    let url = this.api;
    const params: string[] = [];
    if (cuentaId) params.push(`cuentaId=${cuentaId}`);
    if (desde) params.push(`desde=${desde}`);
    if (hasta) params.push(`hasta=${hasta}`);
    if (params.length) url += `?${params.join('&')}`;
    return this.http.get<Movimiento[]>(url);
  }

  // 🔹 Obtener movimiento por Id
  getById(id: string): Observable<Movimiento> {
    return this.http.get<Movimiento>(`${this.api}/${id}`);
  }

  // 🔹 Crear movimiento (crédito o débito)
  create(mov: { cuentaId: string; tipoMovimiento: string; valor: number }): Observable<Movimiento> {
    return this.http.post<Movimiento>(this.api, mov);
  }

  // 🔹 Eliminar movimiento
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
