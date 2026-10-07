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
        bool AgregarPeliculas(Pelicula peli);
        void ActualizarPeliculas(Pelicula peli);
        bool EliminarPeliculas(int id);

    }

    public class PeliculaRepository : IPeliculaRepository
    {
        private PeliculasContext _db;

        public PeliculaRepository(PeliculasContext context)
        {
            _db = context;
        }

        public void ActualizarPeliculas(Pelicula peli)
        {
            _db.Peliculas.Update(peli);
            _db.SaveChanges();
        }

        public bool AgregarPeliculas(Pelicula peli)
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

        public bool EliminarPeliculas(int id)
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
            return _db.Peliculas.ToList();
        }

        public Pelicula? ObtenerPorID(int id)
        {
            Pelicula peli = _db.Peliculas.Find(id);
            return peli;
        }
    }


}
