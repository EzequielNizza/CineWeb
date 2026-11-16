using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cine.data.Modelos
{
    [Table("UsuariosRoles")]
    public class UsuarioRol
    {
        [Key]
        public int IdUsuarioRol { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }
        [Required]
        public int IdRol { get; set; }
        [ForeignKey(nameof(IdRol))]
        public Rol Rol { get; set; }
    }

    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required, StringLength(50)]
        public string Descripcion { get; set; } = "";
        [Column(TypeName = "bit")]
        public bool Borrado { get; set; }
        public List<UsuarioRol> UsuariosRoles { get; set; } = new();
        public List<RolAccion> RolesAcciones { get; set; } = new();
    }

    [Table("RolesAcciones")]
    public class RolAccion
    {
        [Key]
        public int IdRolAccion { get; set; }

        [Required]
        public int IdRol { get; set; }
        [ForeignKey(nameof(IdRol))]
        public Rol Rol { get; set; }

        [Required]
        public int IdAccion { get; set; }
        [ForeignKey(nameof(IdAccion))]
        public Accion Accion { get; set; }
    }

    [Table("Acciones")]
    public class Accion
    {
        [Key]
        public int IdAccion { get; set; }
        [Required, StringLength(100)]
        public string Descripcion { get; set; } = "";

        public List<RolAccion> RolesAcciones { get; set; } = new();
    }
}
