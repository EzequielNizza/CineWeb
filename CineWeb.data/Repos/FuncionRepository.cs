using Cine.data.Modelos;
using Cine.Data.Context;
using Cine.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Cine.Data.Repos
{
    public interface IFuncionRepository
    {
        List<Funcion> ObtenerFunciones();
        Funcion? ObtenerPorID(int id);
        bool AgregarFuncion(Funcion func);
        void ActualizarFuncion(Funcion func);
        bool EliminarFuncion(int id);
    }


    public class FuncionRepository : IFuncionRepository
    {
        private PeliculasContext _db;

        public FuncionRepository(PeliculasContext context)
        {
            _db = context;
        }

        public void ActualizarFuncion(Funcion func)
        {
            _db.Funciones.Update(func);
            _db.SaveChanges();
        }

        public bool AgregarFuncion(Funcion func)
        {
            try
            {
                _db.Funciones.Add(func);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarFuncion(int id)
        {
            try
            {
                Funcion func = _db.Funciones.Find(id);
                if (func != null)
                {
                    _db.Funciones.Remove(func);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Funcion> ObtenerFunciones()
        {
            return _db.Funciones
                      .Include(f => f.Pelicula)
                      .Include(f => f.Sala)
                      .Where(f => !f.Borrado)
                      .ToList();
        }

        public Funcion? ObtenerPorID(int id)
        {
            return _db.Funciones
                      .Include(f => f.Pelicula)
                      .Include(f => f.Sala)
                      .FirstOrDefault(f => f.IdFuncion == id);
        }
    }


}
