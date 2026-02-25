import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { CasaService } from '../../../services/casa/casa.service';
import { CasaDto } from '../../../models/casa/CasaDto.model';
import { MantenimientoCasaEditarComponent } from '../mantenimiento-casa-editar/mantenimiento-casa-editar.component';

@Component({
  selector: 'app-mantenimiento-casa-list',
  imports: [MantenimientoCasaEditarComponent],
  templateUrl: './mantenimiento-casa-list.component.html',
  styleUrls: ['./mantenimiento-casa-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoCasaListComponent implements OnInit {
  private casaService = inject(CasaService);

  casas = signal<CasaDto[]>([]);
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  casaSeleccionada: CasaDto | null = null;

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.casaService.getAll().subscribe({
      next: (data) => this.casas.set(data),
      error: (e) => console.error(e),
    });
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.casaSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEditar(casa: CasaDto): void {
    this.modoEdicion = 'editar';
    this.casaSeleccionada = { ...casa };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.casaSeleccionada = null;
  }

  onGuardado(): void {
    this.cerrarModal();
    this.getAll();
  }

  eliminar(casa: CasaDto): void {
    const ok = window.confirm(`¿Eliminar la casa "${casa.nombre}"?`);
    if (!ok) return;
    this.casaService.delete(casa.id).subscribe({
      next: () => this.getAll(),
      error: (e) => console.error(e),
    });
  }
}
