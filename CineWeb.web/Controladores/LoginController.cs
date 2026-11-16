using Cine.data.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cine.web.Controladores
{
    public class LoginController : Controller
    {
        private IUsuarioRepository _usuarioRepo;
        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepo = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result == null || result.Principal == null)
            {
                return RedirectToAction("Index", "Home"); // O a una vista de error/login
            }

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var nombre = result.Principal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = _usuarioRepo.ObtenerOCrearUsuario(email, nombre);
            if (user == null)
            {
                throw new Exception("No se pudo obtener o crear el usuario en la base de datos.");
            }

            List<String> listaPermisos = _usuarioRepo.ObtenerAcciones(email) ?? new List<String>();

            var claimsPersonalizadas = new List<Claim>(result.Principal.Claims)
    {
        new Claim("IdUsuario", user.IdUsuario.ToString()),
    };

            foreach (var permiso in listaPermisos)
            {
                claimsPersonalizadas.Add(new Claim("Permiso", permiso));
            }

            var identity = new ClaimsIdentity(claimsPersonalizadas, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
