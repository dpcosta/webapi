using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Alura.ListaLeitura.Seguranca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Alura.ListaLeitura.Api.Usuarios
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly SigningConfigurations _signInConfig;
        private readonly TokenConfigurations _tokenConfig;

        public LoginController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            SigningConfigurations signinConfig,
            TokenConfigurations tokenConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signInConfig = signinConfig;
            _tokenConfig = tokenConfig;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(model.Login, "Login"),
                            new[] {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                new Claim(JwtRegisteredClaimNames.UniqueName, model.Login)
                            }
                    );

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao +
                        TimeSpan.FromSeconds(_tokenConfig.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = _tokenConfig.Issuer,
                        Audience = _tokenConfig.Audience,
                        SigningCredentials = _signInConfig.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    return Ok(new
                    {
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        message = "OK"
                    });

                }
                return Unauthorized();
            }
            return BadRequest();
        }

    }
}