using Cine.Data;
using Cine.Data.Repos;
using Cine.Data.Context;
using Cine.Data.Inyecciones;
using Cine.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Cine.data.Modelos;

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
                Descripcion = x.Descripcion,
                DuracionMin = x.DuracionMin

            }).ToList();
            return View(pelis);
        }

        public ActionResult Ver(int id)
        {
            PeliculaVM peli = null;
            var peliDDBB = _peliRepo.ObtenerPorID(id);
            peli = new PeliculaVM
            {
                Id = peliDDBB.IdPelicula,
                Titulo = peliDDBB.Titulo,
                Descripcion = peliDDBB.Descripcion,
                DuracionMin = peliDDBB.DuracionMin
            };
            return View(peli);
        }



        public ActionResult Agregar()
        {
            PeliculaAltaVM peli = new PeliculaAltaVM();
            ViewBag.Mensaje = "Agregar Película";
            return View(peli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(PeliculaAltaVM peli)
        {
            if (ModelState.IsValid)
            {
                Pelicula peliDDBB = new Pelicula
                {
                    Titulo = peli.Titulo,
                    Descripcion = peli.Descripcion,
                    DuracionMin = peli.DuracionMin
           
                };
                bool resultado = _peliRepo.AgregarPelicula(peliDDBB);
                if (resultado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo agregar la película");
                }
            } else
            {
                ModelState.AddModelError("", "Por favor, complete todos los campos requeridos");
            }
            return View(peli);
        }

        public ActionResult Editar(int id)
        {
            PeliculaVM peli = null;
            var peliDDBB = _peliRepo.ObtenerPorID(id);
            peli = new PeliculaVM
            {
                Id = peliDDBB.IdPelicula,
                Titulo = peliDDBB.Titulo,
                Descripcion = peliDDBB.Descripcion,
                DuracionMin = peliDDBB.DuracionMin
            };



            return View(peli);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, PeliculaVM peliEditada)
        {
            try
            {
                Pelicula peliAModificar = new Pelicula();
                peliAModificar.IdPelicula = peliEditada.Id;
                peliAModificar.Titulo = peliEditada.Titulo;
                peliAModificar.Descripcion = peliEditada.Descripcion;
                peliAModificar.DuracionMin = peliEditada.DuracionMin;

                _peliRepo.ActualizarPelicula(peliAModificar);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No se pudo actualizar la película: " + ex.Message);
                return View(peliEditada);
            }
        }

        public ActionResult Borrar(int id)
        {
            PeliculaVM peli = null;
            var peliDDBB = _peliRepo.ObtenerPorID(id);
            peli = new PeliculaVM
            {
                Id = peliDDBB.IdPelicula,
                Titulo = peliDDBB.Titulo,
                Descripcion = peliDDBB.Descripcion,

            };



            return View(peli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Borrar(int id, IFormCollection col)
        {
            try
            {
                var peli = _peliRepo.ObtenerPorID(id);
                peli.Borrado = true;
                _peliRepo.ActualizarPelicula(peli);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "No se pudo borrar la película");
                return View();
            }
    
        }

    }
}