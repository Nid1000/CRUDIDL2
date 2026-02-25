namespace DtoModel.Mascota
{
    public class MascotaTipoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public int UserCreate { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
