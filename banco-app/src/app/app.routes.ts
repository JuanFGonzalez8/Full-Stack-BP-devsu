import { Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

//export const routes: Routes = [];
export const routes: Routes = [
  { path: '', redirectTo: 'clientes', pathMatch: 'full' },
  { path: 'clientes', loadChildren: () => import('./clientes/clientes-module').then(m => m.ClientesModule) },
  { path: 'cuentas', loadChildren: () => import('./cuentas/cuentas-module').then(m => m.CuentasModule) },
  { path: 'movimientos', loadChildren: () => import('./movimientos/movimientos-module').then(m => m.MovimientosModule) },
  { path: 'reportes', loadChildren: () => import('./reportes/reportes-module').then(m => m.ReportesModule) }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }