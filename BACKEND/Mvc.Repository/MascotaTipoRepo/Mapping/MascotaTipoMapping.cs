using DbModel.demoDb;
using DtoModel.Mascota;

namespace Mvc.Repository.MascotaTipoRepo.Mapping
{
    public static class MascotaTipoMapping
    {
        public static MascotaTipo ToEntity(MascotaTipoDto dto)
        {
            return new MascotaTipo
            {
                Id = dto.Id,
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                UserCreate = dto.UserCreate,
                UserUpdate = dto.UserUpdate,
                DateCreated = dto.DateCreated ?? DateTime.Now,
                DateUpdate = dto.DateUpdate
            };
        }

        public static MascotaTipoDto ToDto(MascotaTipo e)
        {
            return new MascotaTipoDto
            {
                Id = e.Id,
                Codigo = e.Codigo,
                Descripcion = e.Descripcion,
                UserCreate = e.UserCreate,
                UserUpdate = e.UserUpdate,
                DateCreated = e.DateCreated,
                DateUpdate = e.DateUpdate
            };
        }

        public static List<MascotaTipoDto> ToDtoList(List<MascotaTipo> data)
        {
            return data.Select(ToDto).ToList();
        }
    }
}
