using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cine.data.Modelos;

namespace Cine.Web.Modelos
{
    public class SalaVM
    {
        public int IdSala { get; set; }
        public int CantidadButacas { get; set; }
        public bool EsIMAX { get; set; }
        public bool Es3D { get; set; }
        public bool Es4DX { get; set; }
        public List<Pelicula> ListaPeliculas { get; set; }
    }
    
    public class SalaAltaVM
    {
        [Required(ErrorMessage = "La cantidad de butacas es obligatoria"), Range(150, 300, ErrorMessage = "Cantidad de butacas fuera de rango")]
        public int CantidadButacas { get; set; }
        public bool EsIMAX { get; set; }
        public bool Es3D { get; set; }
        public bool Es4DX { get; set; }

    }
}
