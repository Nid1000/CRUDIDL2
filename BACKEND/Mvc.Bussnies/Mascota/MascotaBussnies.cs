using DtoModel.Mascota;
using Mvc.Repository.MascotaRepo.Contratos;

namespace Mvc.Bussnies.Mascota
{
    public class MascotaBussnies : IMascotaBussnies
    {
        private readonly IMascotaRepository _repo;

        public MascotaBussnies(IMascotaRepository repo)
        {
            _repo = repo;
        }

        public Task<MascotaDto> Create(MascotaDto request) => _repo.Create(request);
        public Task Delete(int id) => _repo.Delete(id);
        public Task<List<MascotaDto>> GetAll() => _repo.GetAll();
        public Task<MascotaDto?> GetById(int id) => _repo.GetById(id);

        public async Task<MascotaDto?> Update(MascotaDto request)
        {
            var exists = await _repo.GetById(request.Id);
            if (exists == null) return null;
            return await _repo.Update(request);
        }
    }
}
