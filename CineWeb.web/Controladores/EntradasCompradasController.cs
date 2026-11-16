using Cine.Data.Repos;
using Cine.Web.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cine.web.Controladores
{
    [Authorize]
    public class EntradasCompradasController : Controller
    {
        private readonly IEntradaRepository _repo;

        public EntradasCompradasController(IEntradaRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            if (!User.HasClaim("Permiso", "entradas.vertodas"))
            {
                return Forbid();
            }

            var entradasDDBB = _repo.ObtenerTodasLasEntradas();

            List<EntradaCompradaVM> entradas = entradasDDBB.Select(x => new EntradaCompradaVM
            {
                IdEntrada = x.IdEntrada,
                TituloPelicula = x.Funcion?.Pelicula?.Titulo ?? string.Empty,
                IdSala = x.Funcion?.IdSala ?? 0,
                FechaHoraFuncion = x.Funcion?.FechaHora ?? DateTime.MinValue,
                PrecioEntrada = x.PrecioEntrada,
                NombreUsuario = x.Usuario?.Nombre ?? string.Empty,
                EmailUsuario = x.Usuario?.Email ?? string.Empty,
                FechaCompra = x.FechaCompra
            }).ToList();

            return View(entradas);
        }
    }
}
