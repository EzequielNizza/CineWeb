using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cine.data.Modelos
{
    public class Entrada
    {
        [Key]
        public int IdEntrada { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioEntrada { get; set; }
        public int IdFuncion { get; set; }
        [ForeignKey(nameof(IdFuncion))]
        public Funcion Funcion { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }
        public DateTime FechaCompra { get; set; }

    }
}
