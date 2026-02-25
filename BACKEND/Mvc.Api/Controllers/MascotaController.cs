using DtoModel.Mascota;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Mascota;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MascotaController : ControllerBase
    {
        private readonly IMascotaBussnies _buss;

        public MascotaController(IMascotaBussnies buss)
        {
            _buss = buss;
        }

        [HttpGet]
        public async Task<ActionResult<List<MascotaDto>>> GetAll()
        {
            var list = await _buss.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MascotaDto>> GetById(int id)
        {
            var item = await _buss.GetById(id);
            if (item == null) return NotFound(new { message = "Mascota no encontrada" });
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MascotaDto>> Create([FromBody] MascotaDto request)
        {
            var res = await _buss.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut]
        public async Task<ActionResult<MascotaDto>> Update([FromBody] MascotaDto request)
        {
            var res = await _buss.Update(request);
            if (res == null) return NotFound(new { message = "Mascota no encontrada" });
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _buss.GetById(id);
            if (exists == null) return NotFound(new { message = "Mascota no encontrada" });
            await _buss.Delete(id);
            return NoContent();
        }
    }
}
