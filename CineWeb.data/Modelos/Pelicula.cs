using Cine.Data.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cine.data.Modelos
{
    [Table("Peliculas")]
    public class Pelicula
    {
        [Key]
        public int IdPelicula { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        [StringLength(1500)]
        public string? Descripcion { get; set; }
        [Required]
        public int DuracionMin { get; set; }
        [Column(TypeName = "bit")]
        public bool Borrado { get; set; }
        List<Funcion> Funciones { get; set; } = new List<Funcion>();

    }
}
