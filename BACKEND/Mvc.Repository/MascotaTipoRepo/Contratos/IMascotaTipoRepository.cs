using DtoModel.Mascota;

namespace Mvc.Repository.MascotaTipoRepo.Contratos
{
    public interface IMascotaTipoRepository
    {
        Task<List<MascotaTipoDto>> GetAll();
        Task<MascotaTipoDto?> GetById(int id);
        Task<MascotaTipoDto> Create(MascotaTipoDto request);
        Task<MascotaTipoDto> Update(MascotaTipoDto request);
        Task Delete(int id);
    }
}
