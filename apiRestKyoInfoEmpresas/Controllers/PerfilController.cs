using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly PerfilService _perfilService;

        public PerfilController(PerfilService perfilService) =>
            _perfilService = perfilService;

        // GET: api/<PerfilController>
        [HttpGet]
        public async Task<List<Perfil>> Get() =>  await _perfilService.GetAsync();

        // GET api/<PerfilController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Perfil>> Get(string id)
        {
            var perfil = await _perfilService.GetAsync(id);

            if (perfil is null)
            {
                return NotFound();
            }

            return perfil;
        }

        // POST api/<PerfilController>
        [HttpPost]
        public async Task<IActionResult> Post(Perfil newPerfil)
        {
            await _perfilService.CreateAsync(newPerfil);

            return CreatedAtAction(nameof(Get), new { id = newPerfil.Id }, newPerfil);
        }

        // PUT api/<PerfilController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Perfil updatedPerfil)
        {
            var perfil = await _perfilService.GetAsync(id);

            if (perfil is null)
            {
                return NotFound();
            }

            updatedPerfil.Id = perfil.Id;

            await _perfilService.UpdateAsync(id, updatedPerfil);

            return NoContent();
        }

        // DELETE api/<PerfilController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var perfil = await _perfilService.GetAsync(id);

            if (perfil is null)
            {
                return NotFound();
            }

            await _perfilService.RemoveAsync(id);

            return NoContent();
        }
    }
}
