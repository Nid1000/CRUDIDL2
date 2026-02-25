export type CasaDto = {
  id: number;
  nombre: string;
  direccion?: string | null;
  referencia?: string | null;
  idPropietarioPersona?: number | null;

  userCreate?: number;
  userUpdate?: number;
  dateCreated?: string;
  dateUpdate?: string;
};
