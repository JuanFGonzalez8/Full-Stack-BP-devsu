import { Component } from '@angular/core';

save() {
  if (this.form.invalid) return;
  this.svc.create(this.form.value as any).subscribe({
    next: () => this.router.navigate(['/movimientos']),
    error: (err) => alert(err.error ?? 'Error de negocio')
  });
}