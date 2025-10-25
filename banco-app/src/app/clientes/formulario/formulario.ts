import { Component ,OnInit} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'; 
import { FormBuilder } from '@angular/forms';
import { Routes ,ActivatedRoute ,Router} from '@angular/router';

// clientes/form-cliente.component.ts
@Component({ templateUrl: './form-cliente.component.html' })
export class FormClienteComponent implements OnInit {
  form = this.fb.group({
    nombre: ['', Validators.required],
    genero: ['', Validators.required],
    edad: [18, [Validators.required, Validators.min(18)]],
    identificacion: ['', Validators.required],
    direccion: ['', Validators.required],
    telefono: ['', [Validators.required, Validators.pattern(/^\+?\d{7,15}$/)]],
    contrasena: ['', [Validators.required, Validators.minLength(4)]],
    estado: [true]
  });

  constructor(private fb: FormBuilder, private svc: ClientesService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit() { /* cargar si editar */ }

  save() {
    if (this.form.invalid) return;
    const value = this.form.value;
    this.svc.create(value as any).subscribe({
      next: () => this.router.navigate(['/clientes']),
      error: (err) => alert(err.error ?? 'Error al guardar')
    });
  }
}
