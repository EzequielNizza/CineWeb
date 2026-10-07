using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cine.data.Modelos
{
    public class Pelicula
    {
        [Key]
        public int IdPelicula { get; set; }
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        [Required]
        public int DuracionMin { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaFuncion { get; set; }

        [NotMapped]
        public bool EsHoy => FechaFuncion.Date == DateTime.Today;

        [Required, Range(1, 10)]
        public int NumeroSala { get; set; }
        [Required]
        [Range(0, 30000)]
        public decimal Precio { get; set; }
        public bool EsIMAX { get; set; }
        [Column(TypeName = "bit")]
        public bool Borrado { get; set; }

    }
}
