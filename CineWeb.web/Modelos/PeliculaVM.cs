using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine.Web.Modelos
{
    public class PeliculaVM
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int DuracionMin { get; set; }


    }
    public class PeliculaAltaVM
    {
        [Required(ErrorMessage = "El nombre de la película es obligatorio"), StringLength(100)]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración debe ser especificada")]
        public int DuracionMin { get; set; }




    }
}
