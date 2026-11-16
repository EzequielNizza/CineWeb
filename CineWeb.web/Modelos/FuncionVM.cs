using Cine.data.Modelos;
using Cine.Data.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cine.Web.Modelos
{
    public class FuncionVM
    {
        public int IdFuncion { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Borrado { get; set; }
        public int IdPelicula { get; set; }
        public string TituloPelicula { get; set; }
        public int DuracionPelicula { get; set; }
        public int IdSala { get; set; }
        public int CantidadButacas { get; set; }
        public bool EsIMAX { get; set; }
        public bool Es3D { get; set; }
        public bool Es4DX { get; set; }

    }


    public class FuncionAltaVM
    {
        [Required(ErrorMessage = "La fecha y hora son obligatorias")]
        public DateTime FechaHora { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe seleccionar una película")]
        public int IdPelicula { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una sala")]
        public int IdSala { get; set; }
        public List<SelectListItem>? Peliculas { get; set; }
        public List<SelectListItem>? Salas { get; set; }
    }
}
