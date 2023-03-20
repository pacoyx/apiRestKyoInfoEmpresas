using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Models.DTO;
using apiRestKyoInfoEmpresas.Services;
using apiRestKyoInfoEmpresas.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _usuariosService;
        private readonly EmpresaService _empresaService;
        private readonly PerfilService _perfilService;
        private readonly IHelpTools _helpTools;        

        public UsuarioController(UsuarioService usuarioService, EmpresaService empService,PerfilService perfilService, IHelpTools helpTools)
        {
            _usuariosService = usuarioService;
            _empresaService = empService;
            _perfilService = perfilService;
            _helpTools = helpTools;            
        }

        [HttpPost("UsuariosxFechas")]
        public async Task<List<Usuario>> GetUsuarioPorFechas(UsuFechasRequestDto body)
        {
            var ft1 = body.Fecha1;
            var ft2 = body.Fecha2.AddDays(1);          

            var usuarios = await _usuariosService.GetUsuarioPorFecha(ft1 ,ft2);
            return usuarios;
        }


        [HttpGet]        
        public async Task<List<Usuario>> Get() => await _usuariosService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            var usuario = await _usuariosService.GetAsync(id);

            if (usuario is null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario newUsuario)
        {
            DateTime now = DateTime.Now;
            newUsuario.FechaCreacion = now;
            newUsuario.Password = _helpTools.Encrypt(newUsuario.Password);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            await _usuariosService.CreateAsync(newUsuario);

            return CreatedAtAction(nameof(Get), new { id = newUsuario.Id }, newUsuario);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Usuario updatedusuario)
        {
            var user = await _usuariosService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            DateTime now = DateTime.Now;
            updatedusuario.FechaCreacion = now;
            updatedusuario.Id = user.Id;
            updatedusuario.Password = _helpTools.Encrypt(updatedusuario.Password);

            await _usuariosService.UpdateAsync(id, updatedusuario);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _usuariosService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _usuariosService.RemoveAsync(id);

            return NoContent();
        }

    }
}
