using Cine.data.Modelos;
using Cine.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cine.data.Repos
{
    public interface IUsuarioRepository
    {
        Usuario ObtenerOCrearUsuario(string email, string nombre);
        List<String> ObtenerAcciones(string email);
    }
    public class UsuarioRespository : IUsuarioRepository
    {
        private PeliculasContext _db;
        public UsuarioRespository(PeliculasContext context)
        {
            _db = context;
        }

        public List<String> ObtenerAcciones(string Email)
        {
            try
            {
                var usuario = _db.Usuarios.Include(u => u.Roles)
                                      .ThenInclude(ur => ur.Rol)
                                      .ThenInclude(r => r.RolesAcciones)
                                      .ThenInclude(ra => ra.Accion)
                                      .FirstOrDefault(u => u.Email == Email);
                if (usuario == null)
                {
                    return new List<string>();
                }
                var acciones = usuario.Roles
                                      .SelectMany(ur => ur.Rol.RolesAcciones)
                                      .Select(ra => ra.Accion.Descripcion)
                                      .Distinct()
                                      .ToList();
                return acciones;
            }
            catch (Exception ex)
            {

                return new List<string>();
            }
        }

        public Usuario ObtenerOCrearUsuario(string email, string nombre)
        {
            try
            {
                var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == email);
                if (usuario != null)
                {
                    return usuario;
                }

                usuario = new Usuario { Email = email, Nombre = nombre, FechaAlta = DateTime.Now };
                _db.Usuarios.Add(usuario);
                _db.SaveChanges();

                return usuario;
            }
            catch (Exception ex)
            {


                return null;
            }
        }

    }

}
