using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine.Web.Models
{
    public class PeliculaVM
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int DuracionMin { get; set; }
        public DateTime FechaFuncion { get; set; }
        public bool EsHoy => FechaFuncion.Date == DateTime.Today;
        public int NumeroSala { get; set; }
        public decimal Precio { get; set; }
        public bool EsIMAX { get; set; }




    }
    public class PeliculaAltaVM
    {
        [Required(ErrorMessage = "El nombre de la película es obligatorio"), StringLength(100)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La duración debe ser especificada")]
        public int DuracionMin { get; set; }
        [Required]
        public DateTime FechaFuncion { get; set; }

        [Required(ErrorMessage = "El número de sala debe ser especificado"), Range(1, 10, ErrorMessage = "Valor fuera de rango")]
        public int NumeroSala { get; set; }
        [Required(ErrorMessage = "El precio debe ser especificado"), Range(0, 30000, ErrorMessage = "Precio fuera de rango")]
        public decimal Precio { get; set; }


    }
}
