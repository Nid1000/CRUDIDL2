import { ChangeDetectionStrategy, Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CasaDto } from '../../../models/casa/CasaDto.model';
import { CasaService } from '../../../services/casa/casa.service';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { PersonaService } from '../../../services/persona/persona.service';

@Component({
  selector: 'app-mantenimiento-casa-editar',
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-casa-editar.component.html',
  styleUrls: ['./mantenimiento-casa-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoCasaEditarComponent {
  casa = input<CasaDto | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<void>();

  private readonly casaService = inject(CasaService);
  private readonly personaService = inject(PersonaService);
  private readonly fb = inject(FormBuilder);

  personas: PersonaDto[] = [];
  cargando = false;

  readonly form = this.fb.group({
    nombre: ['', [Validators.required]],
    direccion: [''],
    referencia: [''],
    idPropietarioPersona: [null as number | null],
  });

  constructor() {
    this.personaService.getAll().subscribe({
      next: (data) => (this.personas = data),
      error: (e) => console.error(e),
    });

    effect(() => {
      const c = this.casa();
      this.form.reset({
        nombre: c?.nombre ?? '',
        direccion: c?.direccion ?? '',
        referencia: c?.referencia ?? '',
        idPropietarioPersona: c?.idPropietarioPersona ?? null,
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
    const actual = this.casa();
    const nowIso = new Date().toISOString();

    const payload: CasaDto = {
      id: actual?.id ?? 0,
      nombre: v.nombre ?? '',
      direccion: v.direccion,
      referencia: v.referencia,
      idPropietarioPersona: v.idPropietarioPersona,
      userCreate: actual?.userCreate ?? 1,
      userUpdate: actual?.userUpdate ?? 1,
      dateCreated: nowIso,
      dateUpdate: nowIso,
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.casaService.update(payload.id, payload)
      : this.casaService.create(payload);

    request$.subscribe({
      next: () => this.guardado.emit(),
      error: (e) => {
        console.error('Error al guardar casa', e);
        this.cargando = false;
      },
      complete: () => (this.cargando = false),
    });
  }
}
