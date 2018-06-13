using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Alura.WebAPI.Model;
using Alura.WebAPI.Seguranca;
using Alura.WebAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebAPI.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ListaLeituraAuthService _service;
        private readonly string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public UsuarioController(ListaLeituraAuthService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(authScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.LoginAsync(model);
                if (result.Succeeded)
                {
                    //adicionar cookie de autenticação 
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Login),
                        new Claim("Token", result.Content.AccessToken)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, 
                        authScheme
                    );

                    await HttpContext.SignInAsync(
                        authScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties()
                    );

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(String.Empty, "Erro na autenticação");
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(authScheme);
            return RedirectToAction("Login");
        }

    }
}