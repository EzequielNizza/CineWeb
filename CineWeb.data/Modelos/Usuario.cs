using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cine.data.Modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime FechaAlta { get; set; }
        public List<Entrada> Entradas { get; set; } = new List<Entrada>();
        public List<UsuarioRol> Roles { get; set; } = new List<UsuarioRol>();

    }
}
