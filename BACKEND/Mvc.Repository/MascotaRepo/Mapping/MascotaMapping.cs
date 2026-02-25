using DbModel.demoDb;
using DtoModel.Mascota;

namespace Mvc.Repository.MascotaRepo.Mapping
{
    public static class MascotaMapping
    {
        public static Mascota ToEntity(MascotaDto dto)
        {
            return new Mascota
            {
                Id = dto.Id,
                IdDuenioPersona = dto.IdDuenioPersona,
                IdMascotaTipo = dto.IdMascotaTipo,
                IdCasa = dto.IdCasa,
                Nombre = dto.Nombre,
                Sexo = dto.Sexo,
                FechaNacimiento = dto.FechaNacimiento,
                Color = dto.Color,
                Observaciones = dto.Observaciones,
                Estado = dto.Estado,
                UserCreate = dto.UserCreate,
                UserUpdate = dto.UserUpdate,
                DateCreated = dto.DateCreated ?? DateTime.Now,
                DateUpdate = dto.DateUpdate
            };
        }

        public static MascotaDto ToDto(Mascota e)
        {
            var duenio = e.IdDuenioPersonaNavigation;
            var tipo = e.IdMascotaTipoNavigation;
            var casa = e.IdCasaNavigation;

            return new MascotaDto
            {
                Id = e.Id,
                IdDuenioPersona = e.IdDuenioPersona,
                IdMascotaTipo = e.IdMascotaTipo,
                IdCasa = e.IdCasa,
                Nombre = e.Nombre,
                Sexo = e.Sexo,
                FechaNacimiento = e.FechaNacimiento,
                Color = e.Color,
                Observaciones = e.Observaciones,
                Estado = e.Estado,
                UserCreate = e.UserCreate,
                UserUpdate = e.UserUpdate,
                DateCreated = e.DateCreated,
                DateUpdate = e.DateUpdate,
                DuenioNombreCompleto = duenio == null ? null : $"{duenio.Nombres} {duenio.ApellidoPaterno} {duenio.ApellidoMaterno}".Replace("  ", " ").Trim(),
                TipoDescripcion = tipo?.Descripcion,
                CasaNombre = casa?.Nombre
            };
        }

        public static List<MascotaDto> ToDtoList(List<Mascota> data)
        {
            return data.Select(ToDto).ToList();
        }
    }
}
