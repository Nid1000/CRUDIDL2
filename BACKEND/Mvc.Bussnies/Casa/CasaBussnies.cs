using DtoModel.Casa;
using Mvc.Repository.CasaRepo.Contratos;

namespace Mvc.Bussnies.Casa
{
    public class CasaBussnies : ICasaBussnies
    {
        private readonly ICasaRepository _repo;

        public CasaBussnies(ICasaRepository repo)
        {
            _repo = repo;
        }

        public Task<CasaDto> Create(CasaDto request) => _repo.Create(request);

        public Task Delete(int id) => _repo.Delete(id);

        public Task<List<CasaDto>> GetAll() => _repo.GetAll();

        public Task<CasaDto?> GetById(int id) => _repo.GetById(id);

        public async Task<CasaDto?> Update(CasaDto request)
        {
            var exists = await _repo.GetById(request.Id);
            if (exists == null) return null;
            return await _repo.Update(request);
        }
    }
}
