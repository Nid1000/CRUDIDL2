namespace DtoModel.Mascota
{
    public class MascotaDto
    {
        public int Id { get; set; }

        public int IdDuenioPersona { get; set; }
        public int IdMascotaTipo { get; set; }
        public int? IdCasa { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string? Sexo { get; set; } // 'M' o 'H'
        public DateTime? FechaNacimiento { get; set; }
        public string? Color { get; set; }
        public string? Observaciones { get; set; }
        public bool Estado { get; set; } = true;

        public int UserCreate { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdate { get; set; }

        // Campos “de vista” opcionales (para mostrar en frontend)
        public string? DuenioNombreCompleto { get; set; }
        public string? TipoDescripcion { get; set; }
        public string? CasaNombre { get; set; }
    }
}
