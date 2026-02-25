import { Routes } from '@angular/router';
import { MantenimientoPersonaListComponent } from './pages/mantenimiento/mantenimiento-persona-list/mantenimiento-persona-list.component';
import { MantenimientoCasaListComponent } from './pages/mantenimiento/mantenimiento-casa-list/mantenimiento-casa-list.component';
import { MantenimientoMascotaTipoListComponent } from './pages/mantenimiento/mantenimiento-mascota-tipo-list/mantenimiento-mascota-tipo-list.component';
import { MantenimientoMascotaListComponent } from './pages/mantenimiento/mantenimiento-mascota-list/mantenimiento-mascota-list.component';

export const routes: Routes = [

  { path: '', redirectTo: 'personas', pathMatch: 'full' },

  { path: 'personas', component: MantenimientoPersonaListComponent },
  { path: 'casas', component: MantenimientoCasaListComponent },
  { path: 'mascota-tipos', component: MantenimientoMascotaTipoListComponent },
  { path: 'mascotas', component: MantenimientoMascotaListComponent },

  { path: '**', redirectTo: 'personas' },

];
