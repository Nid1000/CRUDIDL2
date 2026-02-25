using DtoModel.Casa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Casa;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CasaController : ControllerBase
    {
        private readonly ICasaBussnies _buss;

        public CasaController(ICasaBussnies buss)
        {
            _buss = buss;
        }

        [HttpGet]
        public async Task<ActionResult<List<CasaDto>>> GetAll()
        {
            var list = await _buss.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CasaDto>> GetById(int id)
        {
            var item = await _buss.GetById(id);
            if (item == null) return NotFound(new { message = "Casa no encontrada" });
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CasaDto>> Create([FromBody] CasaDto request)
        {
            var res = await _buss.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut]
        public async Task<ActionResult<CasaDto>> Update([FromBody] CasaDto request)
        {
            var res = await _buss.Update(request);
            if (res == null) return NotFound(new { message = "Casa no encontrada" });
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _buss.GetById(id);
            if (exists == null) return NotFound(new { message = "Casa no encontrada" });
            await _buss.Delete(id);
            return NoContent();
        }
    }
}
