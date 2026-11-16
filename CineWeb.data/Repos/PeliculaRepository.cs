using Cine.data.Modelos;
using Cine.Data.Context;
using Cine.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Cine.Data.Repos
{
    public interface IPeliculaRepository
    {
        List<Pelicula> ObtenerPeliculas();
        Pelicula? ObtenerPorID(int id);
        bool AgregarPelicula(Pelicula peli);
        void ActualizarPelicula(Pelicula peli);
        bool EliminarPelicula(int id);

    }

    public class PeliculaRepository : IPeliculaRepository
    {
        private PeliculasContext _db;

        public PeliculaRepository(PeliculasContext context)
        {
            _db = context;
        }

        public void ActualizarPelicula(Pelicula peli)
        {
            _db.Peliculas.Update(peli);
            _db.SaveChanges();
        }

        public bool AgregarPelicula(Pelicula peli)
        {
            try
            {
                _db.Peliculas.Add(peli);
                _db.SaveChanges();
                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarPelicula(int id)
        {
            try
            {
                Pelicula peli = _db.Peliculas.Find(id);
                if (peli != null)
                {
                    _db.Peliculas.Remove(peli);
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

        public List<Pelicula> ObtenerPeliculas()
        {
            return _db.Peliculas.Where(p => !p.Borrado).ToList();
        }

        public Pelicula? ObtenerPorID(int id)
        {
            Pelicula peli = _db.Peliculas.Find(id);
            return peli;
        }
    }


}
