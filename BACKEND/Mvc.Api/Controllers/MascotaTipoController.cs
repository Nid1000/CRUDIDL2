using DtoModel.Mascota;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Mascota;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MascotaTipoController : ControllerBase
    {
        private readonly IMascotaTipoBussnies _buss;

        public MascotaTipoController(IMascotaTipoBussnies buss)
        {
            _buss = buss;
        }

        [HttpGet]
        public async Task<ActionResult<List<MascotaTipoDto>>> GetAll()
        {
            var list = await _buss.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MascotaTipoDto>> GetById(int id)
        {
            var item = await _buss.GetById(id);
            if (item == null) return NotFound(new { message = "Tipo de mascota no encontrado" });
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MascotaTipoDto>> Create([FromBody] MascotaTipoDto request)
        {
            var res = await _buss.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut]
        public async Task<ActionResult<MascotaTipoDto>> Update([FromBody] MascotaTipoDto request)
        {
            var res = await _buss.Update(request);
            if (res == null) return NotFound(new { message = "Tipo de mascota no encontrado" });
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _buss.GetById(id);
            if (exists == null) return NotFound(new { message = "Tipo de mascota no encontrado" });
            await _buss.Delete(id);
            return NoContent();
        }
    }
}
