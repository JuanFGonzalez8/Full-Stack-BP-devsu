// clientes.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface Cliente {
  clienteId: string;
  nombre: string;
  genero: string;
  edad: number;
  identificacion: string;
  direccion: string;
  telefono: string;
  estado: boolean;
}

@Injectable({ providedIn: 'root' })
export class ClientesService {
  private api = `${environment.apiUrl}/clientes`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.api);
  }

  getById(id: string): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.api}/${id}`);
  }

  create(cliente: Partial<Cliente>): Observable<Cliente> {
    return this.http.post<Cliente>(this.api, cliente);
  }

  update(id: string, cliente: Cliente): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, cliente);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
