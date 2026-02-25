using DbModel.demoDb;
using DtoModel.Casa;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.CasaRepo.Contratos;
using Mvc.Repository.CasaRepo.Mapping;

namespace Mvc.Repository.CasaRepo.Implementacion
{
    public class CasaRepository : ICasaRepository
    {
        private readonly _demoContext _db;

        public CasaRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<CasaDto> Create(CasaDto request)
        {
            var entity = CasaMapping.ToEntity(request);
            entity.Id = 0;
            await _db.Casa.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CasaMapping.ToDto(entity);
        }

        public async Task Delete(int id)
        {
            await _db.Casa.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<CasaDto>> GetAll()
        {
            var data = await _db.Casa.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return CasaMapping.ToDtoList(data);
        }

        public async Task<CasaDto?> GetById(int id)
        {
            var entity = await _db.Casa.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return entity == null ? null : CasaMapping.ToDto(entity);
        }

        public async Task<CasaDto> Update(CasaDto request)
        {
            var entity = await _db.Casa.FindAsync(request.Id);
            if (entity == null) throw new Exception("Casa no encontrada");

            entity.Nombre = request.Nombre;
            entity.Direccion = request.Direccion;
            entity.Referencia = request.Referencia;
            entity.IdPropietarioPersona = request.IdPropietarioPersona;
            entity.UserUpdate = request.UserUpdate;
            entity.DateUpdate = request.DateUpdate ?? DateTime.Now;

            await _db.SaveChangesAsync();
            return CasaMapping.ToDto(entity);
        }
    }
}
