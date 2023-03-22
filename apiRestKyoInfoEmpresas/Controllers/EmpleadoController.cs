using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Services;
using apiRestKyoInfoEmpresas.Utils;
using Microsoft.AspNetCore.Mvc;

namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;
        private readonly IHelpTools _helpTools;
        public EmpleadoController(EmpleadoService empleadoService, IHelpTools helpTools)
        {
            _empleadoService= empleadoService;
            _helpTools= helpTools;
        }

        // GET: api/<EmpleadoController>
        [HttpGet]
        public async Task<IEnumerable<Empleado>> Get()
        {
            return await _empleadoService.GetAsync();
        }

        // GET api/<EmpleadoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(string id)
        {
            var emp = await _empleadoService.GetAsync(id);
            if(emp is null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        // POST api/<EmpleadoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Empleado emp)
        {
            await _empleadoService.CreateAsync(emp);
            return CreatedAtAction(nameof(Get), new { id = emp.Id}, emp);
        }

        // PUT api/<EmpleadoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Empleado empUpdate)
        {
            var emp = await _empleadoService.GetAsync(id);
            if(emp is null)
            {
                return NotFound();
            }
            await _empleadoService.UpdateAsync(id, empUpdate);
            return NoContent();
        }

        // DELETE api/<EmpleadoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var emp = await _empleadoService.GetAsync(id);
            if (emp is null)
            {
                return NotFound();
            }
            await _empleadoService.RemoveAsync(id);
            return NoContent();
        }
    }
}
