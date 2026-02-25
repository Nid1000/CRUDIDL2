using DbModel.demoDb;
using DtoModel.Casa;

namespace Mvc.Repository.CasaRepo.Mapping
{
    public static class CasaMapping
    {
        public static Casa ToEntity(CasaDto dto)
        {
            return new Casa
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Referencia = dto.Referencia,
                IdPropietarioPersona = dto.IdPropietarioPersona,
                UserCreate = dto.UserCreate,
                UserUpdate = dto.UserUpdate,
                DateCreated = dto.DateCreated ?? DateTime.Now,
                DateUpdate = dto.DateUpdate
            };
        }

        public static CasaDto ToDto(Casa e)
        {
            return new CasaDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Direccion = e.Direccion,
                Referencia = e.Referencia,
                IdPropietarioPersona = e.IdPropietarioPersona,
                UserCreate = e.UserCreate,
                UserUpdate = e.UserUpdate,
                DateCreated = e.DateCreated,
                DateUpdate = e.DateUpdate
            };
        }

        public static List<CasaDto> ToDtoList(List<Casa> data)
        {
            return data.Select(ToDto).ToList();
        }
    }
}
