using Cine.Data;
using Cine.Data.Repos;
using Cine.Data.Context;
using Cine.Data.Inyecciones;
using Cine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Cine.Web.Controllers
{
    public class PeliculasController : Controller
    {
        private IPeliculaRepository _peliRepo;


        public PeliculasController(IPeliculaRepository repo)
        {
            _peliRepo = repo;
        }



        public ActionResult Index()
        {
            ViewBag.Mensaje = "Home";


            List<PeliculaVM> pelis = new List<PeliculaVM>();
            var pelisDDBB = _peliRepo.ObtenerPeliculas();

            pelisDDBB = pelisDDBB.Where(x => x.Borrado == false).ToList();
            pelis = pelisDDBB.Select(x => new PeliculaVM
            {
                Id = x.IdPelicula,
                Titulo = x.Titulo,
                FechaFuncion = x.FechaFuncion,
                DuracionMin = x.DuracionMin

            }).ToList();
            return View(pelis);
        }

    }
}