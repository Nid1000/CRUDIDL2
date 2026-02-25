using DbModel.demoDb;
using DtoModel.Mascota;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.MascotaRepo.Contratos;
using Mvc.Repository.MascotaRepo.Mapping;

namespace Mvc.Repository.MascotaRepo.Implementacion
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly _demoContext _db;

        public MascotaRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<MascotaDto> Create(MascotaDto request)
        {
            var entity = MascotaMapping.ToEntity(request);
            entity.Id = 0;
            if (string.IsNullOrWhiteSpace(entity.Nombre))
                throw new Exception("Nombre de mascota es obligatorio");

            await _db.Mascota.AddAsync(entity);
            await _db.SaveChangesAsync();

            // Recargar con includes para devolver campos de vista
            var full = await _db.Mascota
                .Include(x => x.IdDuenioPersonaNavigation)
                .Include(x => x.IdMascotaTipoNavigation)
                .Include(x => x.IdCasaNavigation)
                .FirstAsync(x => x.Id == entity.Id);

            return MascotaMapping.ToDto(full);
        }

        public async Task Delete(int id)
        {
            await _db.Mascota.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<MascotaDto>> GetAll()
        {
            var data = await _db.Mascota
                .AsNoTracking()
                .Include(x => x.IdDuenioPersonaNavigation)
                .Include(x => x.IdMascotaTipoNavigation)
                .Include(x => x.IdCasaNavigation)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return MascotaMapping.ToDtoList(data);
        }

        public async Task<MascotaDto?> GetById(int id)
        {
            var entity = await _db.Mascota
                .AsNoTracking()
                .Include(x => x.IdDuenioPersonaNavigation)
                .Include(x => x.IdMascotaTipoNavigation)
                .Include(x => x.IdCasaNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : MascotaMapping.ToDto(entity);
        }

        public async Task<MascotaDto> Update(MascotaDto request)
        {
            var entity = await _db.Mascota.FindAsync(request.Id);
            if (entity == null) throw new Exception("Mascota no encontrada");

            entity.IdDuenioPersona = request.IdDuenioPersona;
            entity.IdMascotaTipo = request.IdMascotaTipo;
            entity.IdCasa = request.IdCasa;
            entity.Nombre = request.Nombre;
            entity.Sexo = request.Sexo;
            entity.FechaNacimiento = request.FechaNacimiento;
            entity.Color = request.Color;
            entity.Observaciones = request.Observaciones;
            entity.Estado = request.Estado;
            entity.UserUpdate = request.UserUpdate;
            entity.DateUpdate = request.DateUpdate ?? DateTime.Now;

            await _db.SaveChangesAsync();

            var full = await _db.Mascota
                .Include(x => x.IdDuenioPersonaNavigation)
                .Include(x => x.IdMascotaTipoNavigation)
                .Include(x => x.IdCasaNavigation)
                .FirstAsync(x => x.Id == entity.Id);

            return MascotaMapping.ToDto(full);
        }
    }
}
