import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { MascotaService } from '../../../services/mascota/mascota.service';
import { MascotaDto } from '../../../models/mascota/MascotaDto.model';
import { MantenimientoMascotaEditarComponent } from '../mantenimiento-mascota-editar/mantenimiento-mascota-editar.component';

@Component({
  selector: 'app-mantenimiento-mascota-list',
  imports: [MantenimientoMascotaEditarComponent],
  templateUrl: './mantenimiento-mascota-list.component.html',
  styleUrls: ['./mantenimiento-mascota-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoMascotaListComponent implements OnInit {
  private service = inject(MascotaService);

  mascotas = signal<MascotaDto[]>([]);
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  seleccionado: MascotaDto | null = null;

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.service.getAll().subscribe({
      next: (d) => this.mascotas.set(d),
      error: (e) => console.error(e),
    });
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.seleccionado = null;
    this.mostrarModal = true;
  }

  abrirEditar(item: MascotaDto): void {
    this.modoEdicion = 'editar';
    this.seleccionado = { ...item };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.seleccionado = null;
  }

  onGuardado(): void {
    this.cerrarModal();
    this.getAll();
  }

  eliminar(item: MascotaDto): void {
    const ok = window.confirm(`¿Eliminar la mascota "${item.nombre}"?`);
    if (!ok) return;
    this.service.delete(item.id).subscribe({
      next: () => this.getAll(),
      error: (e) => console.error(e),
    });
  }
}
