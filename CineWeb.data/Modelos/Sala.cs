using Cine.data.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cine.Data.Modelos
{
    [Table("Salas")]
    public class Sala
    {
        [Key]
        public int IdSala { get; set; }
        [Required, Range(150, 300)]
        public int CantidadButacas { get; set; }
        [Column(TypeName = "bit")]
        public bool EsIMAX { get; set; }
        [Column(TypeName = "bit")]
        public bool Es3D { get; set; }
        [Column(TypeName = "bit")]
        public bool Es4DX { get; set; }
        [Column(TypeName = "bit")]
        public bool Borrado { get; set; }
        public List<Funcion> Funciones { get; set; } = new List<Funcion>();
    }
}
