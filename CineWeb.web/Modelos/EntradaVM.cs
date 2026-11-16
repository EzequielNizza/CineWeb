using Cine.data.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cine.Web.Modelos
{
    public class EntradaVM
    {
        public int IdEntrada { get; set; }
        public int IdFuncion { get; set; }
        public string? TituloPelicula { get; set; }
        public DateTime FechaHoraFuncion { get; set; }
        public int IdSala { get; set; }

        public decimal PrecioEntrada { get; set; }
        public DateTime FechaCompra { get; set; }
    }

    public class EntradaCompradaVM
    {
        [Required]
        public int IdEntrada { get; set; }
        [Required]
        public string TituloPelicula { get; set; }
        public int IdSala { get; set; }
        public DateTime FechaHoraFuncion { get; set; }
        public decimal PrecioEntrada { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime FechaCompra { get; set; }
    }
}
