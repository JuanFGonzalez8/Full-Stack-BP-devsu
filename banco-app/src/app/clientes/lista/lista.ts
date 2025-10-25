import { Component,OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'; 
import { MatTableDataSource } from '@angular/material/table';
import { Routes ,ActivatedRoute ,Router} from '@angular/router';

@Component({
  templateUrl: './lista.html'
})
export class ListaClientesComponent implements OnInit {
  dataSource = new MatTableDataSource<ClienteDto>([]);
  displayedColumns = ['nombre','genero','edad','identificacion','direccion','telefono','estado','acciones'];

  constructor(private svc: ClientesService, private router: Router) {}

  ngOnInit() { this.load(); }
  load() { this.svc.getAll().subscribe(data => this.dataSource.data = data); }

  applyFilter(value: string) { this.dataSource.filter = value.trim().toLowerCase(); }

  editar(row: ClienteDto) { this.router.navigate(['/clientes/editar', row.clienteId]); }
  eliminar(row: ClienteDto) {
    this.svc.delete(row.clienteId).subscribe(() => this.load());
  }
}