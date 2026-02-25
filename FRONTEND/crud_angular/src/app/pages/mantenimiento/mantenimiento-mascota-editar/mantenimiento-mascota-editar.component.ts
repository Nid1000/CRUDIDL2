import { ChangeDetectionStrategy, Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MascotaDto } from '../../../models/mascota/MascotaDto.model';
import { MascotaService } from '../../../services/mascota/mascota.service';
import { PersonaService } from '../../../services/persona/persona.service';
import { MascotaTipoService } from '../../../services/mascota/mascota-tipo.service';
import { CasaService } from '../../../services/casa/casa.service';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { MascotaTipoDto } from '../../../models/mascota/MascotaTipoDto.model';
import { CasaDto } from '../../../models/casa/CasaDto.model';

@Component({
  selector: 'app-mantenimiento-mascota-editar',
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-mascota-editar.component.html',
  styleUrls: ['./mantenimiento-mascota-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoMascotaEditarComponent {
  mascota = input<MascotaDto | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<void>();

  private readonly mascotaService = inject(MascotaService);
  private readonly personaService = inject(PersonaService);
  private readonly tipoService = inject(MascotaTipoService);
  private readonly casaService = inject(CasaService);
  private readonly fb = inject(FormBuilder);

  personas: PersonaDto[] = [];
  tipos: MascotaTipoDto[] = [];
  casas: CasaDto[] = [];

  cargando = false;

  readonly form = this.fb.group({
    idDuenioPersona: [null as number | null, [Validators.required]],
    idMascotaTipo: [null as number | null, [Validators.required]],
    idCasa: [null as number | null],
    nombre: ['', [Validators.required]],
    sexo: ['M' as 'M' | 'H'],
    fechaNacimiento: [''],
    color: [''],
    observaciones: [''],
    estado: [true],
  });

  constructor() {
    this.personaService.getAll().subscribe({ next: (d) => (this.personas = d), error: console.error });
    this.tipoService.getAll().subscribe({ next: (d) => (this.tipos = d), error: console.error });
    this.casaService.getAll().subscribe({ next: (d) => (this.casas = d), error: console.error });

    effect(() => {
      const m = this.mascota();
      this.form.reset({
        idDuenioPersona: m?.idDuenioPersona ?? null,
        idMascotaTipo: m?.idMascotaTipo ?? null,
        idCasa: m?.idCasa ?? null,
        nombre: m?.nombre ?? '',
        sexo: (m?.sexo as any) ?? 'M',
        fechaNacimiento: m?.fechaNacimiento ? this.toDateInput(m.fechaNacimiento) : '',
        color: m?.color ?? '',
        observaciones: m?.observaciones ?? '',
        estado: m?.estado ?? true,
      });
    });
  }

  private toDateInput(value: string): string {
    // si viene ISO, nos quedamos con yyyy-MM-dd
    return value.includes('T') ? value.split('T')[0] : value;
  }

  onCancelar(): void {
    this.cancelado.emit();
  }

  onGuardar(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) return;
    this.cargando = true;

    const v = this.form.getRawValue();
    const actual = this.mascota();
    const nowIso = new Date().toISOString();

    const payload: MascotaDto = {
      id: actual?.id ?? 0,
      idDuenioPersona: v.idDuenioPersona!,
      idMascotaTipo: v.idMascotaTipo!,
      idCasa: v.idCasa,
      nombre: v.nombre ?? '',
      sexo: v.sexo,
      fechaNacimiento: v.fechaNacimiento ? (v.fechaNacimiento as string) : null,
      color: v.color,
      observaciones: v.observaciones,
      estado: v.estado ?? true,
      userCreate: actual?.userCreate ?? 1,
      userUpdate: actual?.userUpdate ?? 1,
      dateCreated: nowIso,
      dateUpdate: nowIso,
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.mascotaService.update(payload.id, payload)
      : this.mascotaService.create(payload);

    request$.subscribe({
      next: () => this.guardado.emit(),
      error: (e) => {
        console.error('Error al guardar mascota', e);
        this.cargando = false;
      },
      complete: () => (this.cargando = false),
    });
  }
}
