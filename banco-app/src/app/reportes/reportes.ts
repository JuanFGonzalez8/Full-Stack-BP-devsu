import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface ReporteEstadoCuenta {
  clienteId: string;
  nombre: string;
  cuentas: { numeroCuenta: string; saldoActual: number }[];
  totalDebitos: number;
  totalCreditos: number;
  pdfBase64?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ReportesService {
  private api = `${environment.apiUrl}/reportes`;

  constructor(private http: HttpClient) {}

  // 🔹 Consultar estado de cuenta
  estadoCuenta(clienteId: string, desde: string, hasta: string, incluirPdf = true): Observable<ReporteEstadoCuenta> {
    const url = `${this.api}?clienteId=${clienteId}&desde=${desde}&hasta=${hasta}&incluirPdf=${incluirPdf}`;
    return this.http.get<ReporteEstadoCuenta>(url);
  }
}
