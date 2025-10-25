import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface Cuenta {
  cuentaId: string;
  numeroCuenta: string;
  tipoCuenta: string;
  saldoInicial: number;
  estado: boolean;
  clienteId: string;
}

@Injectable({
  providedIn: 'root'
})
export class CuentasService {
  private api = `${environment.apiUrl}/cuentas`;

  constructor(private http: HttpClient) {}

  // 🔹 Obtener todas las cuentas
  getAll(): Observable<Cuenta[]> {
    return this.http.get<Cuenta[]>(this.api);
  }

  // 🔹 Obtener cuenta por Id
  getById(id: string): Observable<Cuenta> {
    return this.http.get<Cuenta>(`${this.api}/${id}`);
  }

  // 🔹 Listar cuentas por cliente
  getByCliente(clienteId: string): Observable<Cuenta[]> {
    return this.http.get<Cuenta[]>(`${this.api}/por-cliente/${clienteId}`);
  }

  // 🔹 Crear cuenta
  create(cuenta: Partial<Cuenta>): Observable<Cuenta> {
    return this.http.post<Cuenta>(this.api, cuenta);
  }

  // 🔹 Actualizar cuenta (PUT)
  update(id: string, cuenta: Cuenta): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, cuenta);
  }

  // 🔹 Actualizar parcialmente (PATCH)
  patch(id: string, partial: Partial<Cuenta>): Observable<void> {
    return this.http.patch<void>(`${this.api}/${id}`, partial);
  }

  // 🔹 Eliminar cuenta
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
