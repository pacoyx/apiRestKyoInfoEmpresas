using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Services;
using apiRestKyoInfoEmpresas.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {

        private readonly VehiculoService _vehiculoService;
        private readonly IHelpTools _helpTools;
        public VehiculoController(VehiculoService vehiculoService,IHelpTools helpTools)
        {
            _vehiculoService= vehiculoService;
            _helpTools= helpTools;
        }

        // GET: api/<VehiculoController>
        [HttpGet]
        public async Task<IEnumerable<Vehiculo>> Get()
        {
            return await _vehiculoService.GetAsync();
        }

        // GET api/<VehiculoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> Get(string id)
        {
            var vehiculo = await _vehiculoService.GetByIdAsync(id);
            if(vehiculo == null)
            {
                return NotFound();
            }

                return Ok(vehiculo);
        }

        // POST api/<VehiculoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _vehiculoService.CreateAsync(vehiculo);

            return CreatedAtAction(nameof(Get), new { id = vehiculo.Id }, vehiculo);
        }

        // PUT api/<VehiculoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Vehiculo vehiculoUpdate)
        {
            var vehiculo = await _vehiculoService.GetByIdAsync(id);
            if (vehiculo is null)
            {
                return NotFound();
            }
            await _vehiculoService.UpdateAsync(id, vehiculoUpdate);
            return NoContent();
        }

        // DELETE api/<VehiculoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var vehiculo = await _vehiculoService.GetByIdAsync(id);
            if (vehiculo is null)
            {
                return NotFound();
            }
            await _vehiculoService.RemoveAsync(id);
            return NoContent();

        }
    }
}
