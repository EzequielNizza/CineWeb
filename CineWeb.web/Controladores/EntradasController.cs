using Cine.data.Modelos;
using Cine.data.Repos;
using Cine.Data.Repos;
using Cine.Web.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cine.web.Controladores
{
   [Authorize]
    public class EntradasController : Controller
    {
        private readonly IEntradaRepository _repo;
        private readonly IFuncionRepository _funcionRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EntradasController(IEntradaRepository repository, IFuncionRepository funcionRepository, IUsuarioRepository usuarioRepository)
        {
            _repo = repository;
            _funcionRepository = funcionRepository;
            _usuarioRepository = usuarioRepository;
        }

        private Usuario ObtenerUsuarioLogueado()
        {
            return _usuarioRepository.ObtenerOCrearUsuario(
                User.FindFirstValue(ClaimTypes.Email),
                User.FindFirstValue(ClaimTypes.Name));
        }

        public IActionResult Index()
        {
            var funcionesDDBB = _funcionRepository.ObtenerFunciones()
                                                  .Where(x => !x.Borrado)
                                                  .ToList();

            List<FuncionVM> funciones = funcionesDDBB.Select(x => new FuncionVM
            {
                IdFuncion = x.IdFuncion,
                FechaHora = x.FechaHora,
                IdPelicula = x.IdPelicula,
                TituloPelicula = x.Pelicula != null ? x.Pelicula.Titulo : "Sin título",
                IdSala = x.IdSala
            }).ToList();

            return View(funciones);
        }

        [HttpGet("/Entrada/Comprar/{idFuncion:int}")]
        public IActionResult Comprar(int idFuncion)
        {
            var funcionDDBB = _funcionRepository.ObtenerPorID(idFuncion);
            if (funcionDDBB == null || funcionDDBB.Borrado)
            {
                return NotFound();
            }

            EntradaVM entrada = new EntradaVM
            {
                IdFuncion = funcionDDBB.IdFuncion,
                TituloPelicula = funcionDDBB.Pelicula?.Titulo ?? string.Empty,
                FechaHoraFuncion = funcionDDBB.FechaHora,
                IdSala = funcionDDBB.IdSala,
                PrecioEntrada = 5000
            };

            return View(entrada);
        }

        [HttpPost("/Entrada/Comprar")]
        [ValidateAntiForgeryToken]
        public IActionResult ComprarConfirmado(EntradaVM entrada)
        {
            var usuario = ObtenerUsuarioLogueado();

            Entrada entradaDDBB = new Entrada
            {
                IdFuncion = entrada.IdFuncion,
                IdUsuario = usuario?.IdUsuario ?? 0,
                PrecioEntrada = entrada.PrecioEntrada
            };

            var result = usuario != null && _repo.ComprarEntrada(entradaDDBB);

            if (!result)
            {
                TempData["Mensaje"] = "No se pudo comprar la entrada";
                return RedirectToAction(nameof(Index));
            }

            TempData["Mensaje"] = "Entrada comprada con éxito";
            return RedirectToAction(nameof(MisEntradas));
        }

        public IActionResult MisEntradas()
        {
            var usuario = ObtenerUsuarioLogueado();
            var entradasDDBB = _repo.ObtenerEntradasPorUsuario(usuario.IdUsuario);

            List<EntradaVM> entradas = entradasDDBB.Select(x => new EntradaVM
            {
                IdEntrada = x.IdEntrada,
                IdFuncion = x.IdFuncion,
                TituloPelicula = x.Funcion?.Pelicula?.Titulo ?? "Sin título",
                IdSala = x.Funcion?.IdSala ?? 0,
                FechaHoraFuncion = x.Funcion?.FechaHora ?? DateTime.MinValue,
                PrecioEntrada = x.PrecioEntrada,
                FechaCompra = x.FechaCompra
            }).ToList();

            return View(entradas);
        }

        public IActionResult TodasLasEntradas()
        {
            if (!User.HasClaim("Permiso", "entradas.ver"))
            {
                return Forbid();
            }

            var entradasDDBB = _repo.ObtenerTodasLasEntradas();

            List<EntradaCompradaVM> entradas = entradasDDBB.Select(x => new EntradaCompradaVM
            {
                IdEntrada = x.IdEntrada,
                TituloPelicula = x.Funcion?.Pelicula?.Titulo ?? "Sin título",
                IdSala = x.Funcion?.IdSala ?? 0,
                FechaHoraFuncion = x.Funcion?.FechaHora ?? DateTime.MinValue,
                PrecioEntrada = x.PrecioEntrada,
                NombreUsuario = x.Usuario?.Nombre ?? "Desconocido",
                EmailUsuario = x.Usuario?.Email ?? "",
                FechaCompra = x.FechaCompra
            }).ToList();

            return View(entradas);
        }
    }
}
