using Cine.data.Modelos;
using Cine.data.Repos;
using Cine.Data.Modelos;
using Cine.Data.Repos;
using Cine.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine.web.Controladores
{
    public class FuncionesController : Controller
    {
        private readonly IFuncionRepository _funcionRepo;
        private readonly IPeliculaRepository _peliRepo;
        private readonly ISalaRepository _salaRepo;

        public FuncionesController(IFuncionRepository funcionRepo, IPeliculaRepository peliRepo, ISalaRepository salaRepo)
        {
            _funcionRepo = funcionRepo;
            _peliRepo = peliRepo;
            _salaRepo = salaRepo;
        }

        public ActionResult Index()
        {
            ViewBag.Mensaje = "Listado de Funciones";

            var funcionesDDBB = _funcionRepo.ObtenerFunciones();

            var funciones = funcionesDDBB.Select(x => new FuncionVM
            {
                IdFuncion = x.IdFuncion,
                FechaHora = x.FechaHora,
                IdPelicula = x.IdPelicula,
                TituloPelicula = x.Pelicula != null ? x.Pelicula.Titulo : string.Empty,
                IdSala = x.IdSala
            }).ToList();
            return View(funciones);
        }

        public void CargarListas(FuncionAltaVM vm)
        {
            var pelis = _peliRepo.ObtenerPeliculas();
            var salas = _salaRepo.ObtenerSalas();

            vm.Peliculas = pelis.Select(x => new SelectListItem
            {
                Value = x.IdPelicula.ToString(),
                Text = x.Titulo
            }).ToList();

            vm.Salas = salas.Select(x => new SelectListItem
            {
                Value = x.IdSala.ToString(),
                Text = x.IdSala.ToString(),
            }).ToList();
        }
        public ActionResult Ver(int id)
        {
            var funcionDDBB = _funcionRepo.ObtenerPorID(id);
            if (funcionDDBB == null)
            {
                return NotFound();
            }
            FuncionVM vm = new FuncionVM
            {
                IdFuncion = funcionDDBB.IdFuncion,
                FechaHora = funcionDDBB.FechaHora,
                IdPelicula = funcionDDBB.IdPelicula,
                TituloPelicula = funcionDDBB.Pelicula.Titulo ?? string.Empty,
                DuracionPelicula = funcionDDBB.Pelicula.DuracionMin,
                IdSala = funcionDDBB.IdSala,
                CantidadButacas = funcionDDBB.Sala.CantidadButacas,
                EsIMAX = funcionDDBB.Sala.EsIMAX,
                Es3D = funcionDDBB.Sala.Es3D,
                Es4DX = funcionDDBB.Sala.Es4DX
            };
            return View(vm);
        }

        public ActionResult Agregar()
        {
            FuncionAltaVM vm = new FuncionAltaVM();
            CargarListas(vm);
            ViewBag.Mensaje = "Agregar Función";
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(FuncionAltaVM funcionVM)
        {
            if (ModelState.IsValid)
            {
                Funcion funcionDDBB = new Funcion
                {
                    FechaHora = funcionVM.FechaHora,
                    IdPelicula = funcionVM.IdPelicula,
                    IdSala = funcionVM.IdSala
                };

                bool resultado = _funcionRepo.AgregarFuncion(funcionDDBB);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo agregar la función");
                }
            }
            else
            {
                ModelState.AddModelError("", "Por favor, complete todos los campos requeridos");
            }

            CargarListas(funcionVM);
            return View(funcionVM);
        }

        public ActionResult Editar(int id)
        {
            var funcionDDBB = _funcionRepo.ObtenerPorID(id);
            if (funcionDDBB == null)
            {
                return NotFound();
            }

            FuncionVM func = new FuncionVM
            {
                IdFuncion = funcionDDBB.IdFuncion,
                FechaHora = funcionDDBB.FechaHora,
                IdPelicula = funcionDDBB.IdPelicula,
                TituloPelicula = funcionDDBB.Pelicula.Titulo ?? string.Empty,
                DuracionPelicula = funcionDDBB.Pelicula.DuracionMin,
                IdSala = funcionDDBB.IdSala,
                CantidadButacas = funcionDDBB.Sala.CantidadButacas,
                EsIMAX = funcionDDBB.Sala.EsIMAX,
                Es3D = funcionDDBB.Sala.Es3D,
                Es4DX = funcionDDBB.Sala.Es4DX
            };

            return View(func);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, FuncionVM funcionEditada)
        {
            try
            {
                Funcion funcionAModificar = new Funcion
                {
                    IdFuncion = funcionEditada.IdFuncion,
                    FechaHora = funcionEditada.FechaHora,
                    IdPelicula = funcionEditada.IdPelicula,
                    IdSala = funcionEditada.IdSala
                };

                _funcionRepo.ActualizarFuncion(funcionAModificar);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No se pudo actualizar la función: " + ex.Message);
                return View(funcionEditada);

            }
        }

        public ActionResult Borrar(int id)
        {
            FuncionVM funcion = null;
            var funcionDDBB = _funcionRepo.ObtenerPorID(id);
            funcion = new FuncionVM
            {
                IdFuncion = funcionDDBB.IdFuncion,
                FechaHora = funcionDDBB.FechaHora,
                IdPelicula = funcionDDBB.IdPelicula,
                TituloPelicula = funcionDDBB.Pelicula.Titulo ?? string.Empty,
                DuracionPelicula = funcionDDBB.Pelicula.DuracionMin,
                IdSala = funcionDDBB.IdSala,
                CantidadButacas = funcionDDBB.Sala.CantidadButacas,
                EsIMAX = funcionDDBB.Sala.EsIMAX,
                Es3D = funcionDDBB.Sala.Es3D,
                Es4DX = funcionDDBB.Sala.Es4DX
            };



            return View(funcion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id, IFormCollection col)
        {
            try
            {
                var funcion = _funcionRepo.ObtenerPorID(id);
                funcion.Borrado = true;
                _funcionRepo.ActualizarFuncion(funcion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "No se pudo borrar la función");
                return View();
            }

        }
    }
}
