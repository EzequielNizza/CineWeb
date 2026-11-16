using Cine.data.Modelos;
using Cine.Data.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cine.Data.Context
{
    public class PeliculasContext : DbContext
    {
        public PeliculasContext(DbContextOptions<PeliculasContext> options) : base(options) { }
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Sala> Salas => Set<Sala>();
        public DbSet<Funcion> Funciones => Set<Funcion>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Entrada> Entradas => Set<Entrada>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Accion> Acciones => Set<Accion>();
        public DbSet<UsuarioRol> UsuariosRoles => Set<UsuarioRol>();
        public DbSet<RolAccion> RolesAcciones => Set<RolAccion>();
    }
}
