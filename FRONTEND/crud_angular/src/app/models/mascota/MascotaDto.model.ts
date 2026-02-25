export type MascotaDto = {
  id: number;
  idDuenioPersona: number;
  idMascotaTipo: number;
  idCasa?: number | null;

  nombre: string;
  sexo?: 'M' | 'H' | null;
  fechaNacimiento?: string | null; // ISO o yyyy-MM-dd
  color?: string | null;
  observaciones?: string | null;
  estado?: boolean;

  userCreate?: number;
  userUpdate?: number;
  dateCreated?: string;
  dateUpdate?: string;

  // campos de vista
  duenioNombreCompleto?: string | null;
  tipoDescripcion?: string | null;
  casaNombre?: string | null;
};
