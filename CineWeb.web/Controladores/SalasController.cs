using Cine.data.Repos;
using Cine.Data.Modelos;
using Cine.Web.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Web.Controllers
{
    public class SalasController : Controller
    {
        private readonly ISalaRepository _salaRepo;

        public SalasController(ISalaRepository salaRepository)
        {
            _salaRepo = salaRepository;
        }

        public ActionResult Index()
        {

            ViewBag.Mensaje = "Home";


            List<SalaVM> salas = new List<SalaVM>();
            var salasDDBB = _salaRepo.ObtenerSalas();

            salasDDBB = salasDDBB.ToList();
            salas = salasDDBB.Select(x => new SalaVM
            {
                IdSala = x.IdSala,
                CantidadButacas = x.CantidadButacas
            }).ToList();
            return View(salas);
        }

        public ActionResult Ver(int id)
        {
            var salaDDBB = _salaRepo.ObtenerPorID(id);
            if (salaDDBB == null)
            {
                return NotFound();
            }

            var sala = new SalaVM
            {
                IdSala = salaDDBB.IdSala,
                CantidadButacas = salaDDBB.CantidadButacas,
                EsIMAX = salaDDBB.EsIMAX,
                Es3D = salaDDBB.Es3D,
                Es4DX = salaDDBB.Es4DX
            };

            return View(sala);
        }

        public ActionResult Agregar()
        {
            SalaAltaVM sala = new SalaAltaVM();
            ViewBag.Mensaje = "Agregar Sala";
            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(SalaAltaVM sala)
        {
            if (ModelState.IsValid)
            {
                Sala salaDDBB = new Sala
                {
                    CantidadButacas = sala.CantidadButacas,
                    EsIMAX = sala.EsIMAX,
                    Es3D = sala.Es3D,
                    Es4DX = sala.Es4DX
                };

                bool resultado = _salaRepo.AgregarSala(salaDDBB);
                if (resultado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo agregar la sala");
                }
            }
            else
            {
                ModelState.AddModelError("", "Por favor, complete todos los campos requeridos o verifique el rango de butacas");
            }
            return View(sala);
        }

        public ActionResult Editar(int id)
        {
            var salaDDBB = _salaRepo.ObtenerPorID(id);
            if (salaDDBB == null)
            {
                return NotFound();
            }

            SalaVM sala = new SalaVM
            {
                IdSala = salaDDBB.IdSala,
                CantidadButacas = salaDDBB.CantidadButacas,
                EsIMAX = salaDDBB.EsIMAX,
                Es3D = salaDDBB.Es3D,
                Es4DX = salaDDBB.Es4DX
            };

            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, SalaVM salaEditada)
        {
            try
            {
                Sala salaAModificar = new Sala
                {
                    IdSala = salaEditada.IdSala,
                    CantidadButacas = salaEditada.CantidadButacas,
                    EsIMAX = salaEditada.EsIMAX,
                    Es3D = salaEditada.Es3D,
                    Es4DX = salaEditada.Es4DX
                };

                _salaRepo.ActualizarSala(salaAModificar);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No se pudo actualizar la sala: " + ex.Message);
                return View(salaEditada);
            }
        }

        public ActionResult Borrar(int id)
        {
            SalaVM sala = null;
            var salaDDBB = _salaRepo.ObtenerPorID(id);
            sala = new SalaVM
            {
                IdSala = salaDDBB.IdSala,
                CantidadButacas = salaDDBB.CantidadButacas,
                EsIMAX = salaDDBB.EsIMAX,
                Es3D = salaDDBB.Es3D,
                Es4DX = salaDDBB.Es4DX
            };

            return View(sala);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id, IFormCollection col)
        {
            try
            {
                var sala = _salaRepo.ObtenerPorID(id);
                if (sala != null)
                {
                    sala.Borrado = true; 
                    _salaRepo.ActualizarSala(sala);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "No se pudo borrar la sala");
                return View();
            }
        }

    }
}