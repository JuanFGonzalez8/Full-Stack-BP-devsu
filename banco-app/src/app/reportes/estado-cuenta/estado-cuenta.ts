import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators ,FormBuilder } from '@angular/forms'; 


// reportes/estado-cuenta.component.ts
export class EstadoCuentaComponent {
  form = this.fb.group({
    clienteId: ['', Validators.required],
    desde: ['', Validators.required],
    hasta: ['', Validators.required],
    incluirPdf: [true]
  });
  data?: ReporteEstadoCuentaResponse;

  constructor(private fb: FormBuilder, private svc: ReportesService) {}

  consultar() {
    const { clienteId, desde, hasta, incluirPdf } = this.form.value as any;
    this.svc.estadoCuenta(clienteId, desde, hasta, incluirPdf).subscribe(resp => this.data = resp);
  }

  descargarPdf() {
    if (!this.data?.pdfBase64) return;
    const byteChars = atob(this.data.pdfBase64);
    const byteNums = Array.from(byteChars).map(c => c.charCodeAt(0));
    const blob = new Blob([new Uint8Array(byteNums)], { type: 'application/pdf' });
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = 'estado-cuenta.pdf';
    link.click();
  }
}
