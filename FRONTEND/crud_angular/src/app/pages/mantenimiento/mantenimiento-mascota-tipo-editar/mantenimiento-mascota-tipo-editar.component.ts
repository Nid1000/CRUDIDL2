import { ChangeDetectionStrategy, Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MascotaTipoDto } from '../../../models/mascota/MascotaTipoDto.model';
import { MascotaTipoService } from '../../../services/mascota/mascota-tipo.service';

@Component({
  selector: 'app-mantenimiento-mascota-tipo-editar',
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-mascota-tipo-editar.component.html',
  styleUrls: ['./mantenimiento-mascota-tipo-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoMascotaTipoEditarComponent {
  tipo = input<MascotaTipoDto | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<void>();

  private readonly service = inject(MascotaTipoService);
  private readonly fb = inject(FormBuilder);

  cargando = false;

  readonly form = this.fb.group({
    codigo: ['', [Validators.required]],
    descripcion: ['', [Validators.required]],
  });

  constructor() {
    effect(() => {
      const t = this.tipo();
      this.form.reset({
        codigo: t?.codigo ?? '',
        descripcion: t?.descripcion ?? '',
      });
    });
  }

  onCancelar(): void {
    this.cancelado.emit();
  }

  onGuardar(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) return;
    this.cargando = true;

    const v = this.form.getRawValue();
    const actual = this.tipo();
    const nowIso = new Date().toISOString();

    const payload: MascotaTipoDto = {
      id: actual?.id ?? 0,
      codigo: (v.codigo ?? '').trim().toUpperCase(),
      descripcion: (v.descripcion ?? '').trim(),
      userCreate: actual?.userCreate ?? 1,
      userUpdate: actual?.userUpdate ?? 1,
      dateCreated: nowIso,
      dateUpdate: nowIso,
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.service.update(payload.id, payload)
      : this.service.create(payload);

    request$.subscribe({
      next: () => this.guardado.emit(),
      error: (e) => {
        console.error('Error al guardar tipo mascota', e);
        this.cargando = false;
      },
      complete: () => (this.cargando = false),
    });
  }
}
