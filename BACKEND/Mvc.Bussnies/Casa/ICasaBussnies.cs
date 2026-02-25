using DtoModel.Casa;

namespace Mvc.Bussnies.Casa
{
    public interface ICasaBussnies
    {
        Task<List<CasaDto>> GetAll();
        Task<CasaDto?> GetById(int id);
        Task<CasaDto> Create(CasaDto request);
        Task<CasaDto?> Update(CasaDto request);
        Task Delete(int id);
    }
}
