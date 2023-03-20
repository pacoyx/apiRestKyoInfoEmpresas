using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Models.DTO;
using apiRestKyoInfoEmpresas.Services;
using apiRestKyoInfoEmpresas.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace apiRestKyoInfoEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> logger;
        private readonly ILoginService _loginService;
        private readonly IHelpTools _helpTools;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IHelpTools helpTools,
            ILoginService loginService
            )
        {            
            this.logger = logger;
            _helpTools = helpTools;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            logger.LogInformation("Inside Login endpoint");


            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }


            var respLogin = ValidaUsuario(user).Result;
            if (respLogin.Estado)
            {
                var permClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, respLogin.Nombre),
                    new Claim(ClaimTypes.Email, respLogin.Correo),
                    new Claim(ClaimTypes.Role, respLogin.Perfil)
                };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    claims: permClaims,
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                var resp = new
                {
                    data = new JWTTokenResponse { Token = tokenString },
                    usuario = respLogin.Nombre,
                    correo = respLogin.Correo,
                    role = respLogin.Perfil
                };

                return Ok(resp);
            }
            return Unauthorized();
        }


        async private Task<LoginResponseDto> ValidaUsuario(Login user)
        {
            logger.LogInformation("Inside ValidaUsuario");
            LoginResponseDto loginResp = await _loginService.Login(user.UserName, _helpTools.Encrypt(user.Password));            
            return loginResp;
        }

    }
}
