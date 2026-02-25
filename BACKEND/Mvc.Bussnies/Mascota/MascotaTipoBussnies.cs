using DtoModel.Mascota;
using Mvc.Repository.MascotaTipoRepo.Contratos;

namespace Mvc.Bussnies.Mascota
{
    public class MascotaTipoBussnies : IMascotaTipoBussnies
    {
        private readonly IMascotaTipoRepository _repo;

        public MascotaTipoBussnies(IMascotaTipoRepository repo)
        {
            _repo = repo;
        }

        public Task<MascotaTipoDto> Create(MascotaTipoDto request) => _repo.Create(request);
        public Task Delete(int id) => _repo.Delete(id);
        public Task<List<MascotaTipoDto>> GetAll() => _repo.GetAll();
        public Task<MascotaTipoDto?> GetById(int id) => _repo.GetById(id);

        public async Task<MascotaTipoDto?> Update(MascotaTipoDto request)
        {
            var exists = await _repo.GetById(request.Id);
            if (exists == null) return null;
            return await _repo.Update(request);
        }
    }
}
