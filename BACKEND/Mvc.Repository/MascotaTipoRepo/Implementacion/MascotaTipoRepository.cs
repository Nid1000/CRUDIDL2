using DbModel.demoDb;
using DtoModel.Mascota;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.MascotaTipoRepo.Contratos;
using Mvc.Repository.MascotaTipoRepo.Mapping;

namespace Mvc.Repository.MascotaTipoRepo.Implementacion
{
    public class MascotaTipoRepository : IMascotaTipoRepository
    {
        private readonly _demoContext _db;

        public MascotaTipoRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<MascotaTipoDto> Create(MascotaTipoDto request)
        {
            var entity = MascotaTipoMapping.ToEntity(request);
            entity.Id = 0;
            entity.Codigo = entity.Codigo?.Trim().ToUpperInvariant() ?? string.Empty;
            await _db.MascotaTipo.AddAsync(entity);
            await _db.SaveChangesAsync();
            return MascotaTipoMapping.ToDto(entity);
        }

        public async Task Delete(int id)
        {
            await _db.MascotaTipo.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<MascotaTipoDto>> GetAll()
        {
            var data = await _db.MascotaTipo.AsNoTracking().OrderBy(x => x.Descripcion).ToListAsync();
            return MascotaTipoMapping.ToDtoList(data);
        }

        public async Task<MascotaTipoDto?> GetById(int id)
        {
            var entity = await _db.MascotaTipo.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return entity == null ? null : MascotaTipoMapping.ToDto(entity);
        }

        public async Task<MascotaTipoDto> Update(MascotaTipoDto request)
        {
            var entity = await _db.MascotaTipo.FindAsync(request.Id);
            if (entity == null) throw new Exception("Tipo de mascota no encontrado");

            entity.Codigo = request.Codigo?.Trim().ToUpperInvariant() ?? entity.Codigo;
            entity.Descripcion = request.Descripcion;
            entity.UserUpdate = request.UserUpdate;
            entity.DateUpdate = request.DateUpdate ?? DateTime.Now;

            await _db.SaveChangesAsync();
            return MascotaTipoMapping.ToDto(entity);
        }
    }
}
