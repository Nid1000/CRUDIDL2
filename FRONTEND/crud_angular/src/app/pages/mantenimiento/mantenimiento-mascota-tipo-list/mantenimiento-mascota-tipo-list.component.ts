import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { MascotaTipoService } from '../../../services/mascota/mascota-tipo.service';
import { MascotaTipoDto } from '../../../models/mascota/MascotaTipoDto.model';
import { MantenimientoMascotaTipoEditarComponent } from '../mantenimiento-mascota-tipo-editar/mantenimiento-mascota-tipo-editar.component';

@Component({
  selector: 'app-mantenimiento-mascota-tipo-list',
  imports: [MantenimientoMascotaTipoEditarComponent],
  templateUrl: './mantenimiento-mascota-tipo-list.component.html',
  styleUrls: ['./mantenimiento-mascota-tipo-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoMascotaTipoListComponent implements OnInit {
  private service = inject(MascotaTipoService);

  tipos = signal<MascotaTipoDto[]>([]);
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  seleccionado: MascotaTipoDto | null = null;

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.service.getAll().subscribe({
      next: (d) => this.tipos.set(d),
      error: (e) => console.error(e),
    });
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.seleccionado = null;
    this.mostrarModal = true;
  }

  abrirEditar(item: MascotaTipoDto): void {
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

  eliminar(item: MascotaTipoDto): void {
    const ok = window.confirm(`¿Eliminar el tipo "${item.descripcion}"?`);
    if (!ok) return;
    this.service.delete(item.id).subscribe({
      next: () => this.getAll(),
      error: (e) => console.error(e),
    });
  }
}
