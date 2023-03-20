using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {


        private readonly EmpresaService _empresaService;


        public EmpresaController(EmpresaService empresaService) =>
            _empresaService = empresaService;

        // GET: api/<EmpresaController>
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Authorize]
        public async Task<List<Empresa>> Get() =>  await _empresaService.GetAsync();

        [HttpPost]
        public async Task<IActionResult> Post(Empresa newEmpresa)
        {
            await _empresaService.CreateAsync(newEmpresa);

            return CreatedAtAction(nameof(Get), new { id = newEmpresa.Id }, newEmpresa);
        }

    }
}
