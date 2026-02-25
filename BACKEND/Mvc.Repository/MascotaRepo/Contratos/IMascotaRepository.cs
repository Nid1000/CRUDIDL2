using DtoModel.Mascota;

namespace Mvc.Repository.MascotaRepo.Contratos
{
    public interface IMascotaRepository
    {
        Task<List<MascotaDto>> GetAll();
        Task<MascotaDto?> GetById(int id);
        Task<MascotaDto> Create(MascotaDto request);
        Task<MascotaDto> Update(MascotaDto request);
        Task Delete(int id);
    }
}
