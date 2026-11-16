using Cine.Data.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;

namespace Cine.data.Modelos
{
    [Table("Funciones")]
    public class Funcion
    {
        [Key]
        public int IdFuncion { get; set; }
        [Required]
        public DateTime FechaHora { get; set; }

        [Column(TypeName = "bit")]
        public bool Borrado { get; set; }

        public int IdPelicula { get; set; }
        [ForeignKey(nameof(IdPelicula))]
        public Pelicula Pelicula { get; set; }
        public int IdSala { get; set; }
        [ForeignKey(nameof(IdSala))]
        public Sala Sala { get; set; }
        public List<Entrada> Entradas { get; set; } = new List<Entrada>();
    }
}
