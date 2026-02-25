using DtoModel.Mascota;

namespace Mvc.Bussnies.Mascota
{
    public interface IMascotaTipoBussnies
    {
        Task<List<MascotaTipoDto>> GetAll();
        Task<MascotaTipoDto?> GetById(int id);
        Task<MascotaTipoDto> Create(MascotaTipoDto request);
        Task<MascotaTipoDto?> Update(MascotaTipoDto request);
        Task Delete(int id);
    }
}
