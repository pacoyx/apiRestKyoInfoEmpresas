using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuiaRecojoController : ControllerBase
    {

        private readonly GuiaService _guiaService;

        public GuiaRecojoController(GuiaService guiaService) =>
            _guiaService = guiaService;

        [HttpGet]
        public async Task<List<GuiaRecojo>> Get() =>
            await _guiaService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<GuiaRecojo>> Get(string id)
        {
            var guia = await _guiaService.GetAsync(id);

            if (guia is null)
            {
                return NotFound();
            }

            return guia;
        }

        [Authorize(Roles = "cliente,admin")]
        [HttpPost]
        public async Task<IActionResult> Post(GuiaRecojo newGuia)
        {
            await _guiaService.CreateAsync(newGuia);

            return CreatedAtAction(nameof(Get), new { id = newGuia.Id }, newGuia);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, GuiaRecojo updatedGuia)
        {
            var guia = await _guiaService.GetAsync(id);

            if (guia is null)
            {
                return NotFound();
            }

            updatedGuia.Id = guia.Id;

            await _guiaService.UpdateAsync(id, updatedGuia);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var guia = await _guiaService.GetAsync(id);

            if (guia is null)
            {
                return NotFound();
            }

            await _guiaService.RemoveAsync(id);

            return NoContent();
        }

    }
}
