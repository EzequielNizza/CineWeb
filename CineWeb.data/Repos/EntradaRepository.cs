using Cine.data.Modelos;
using Cine.Data.Context;
using Cine.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Cine.Data.Repos
{
    public interface IEntradaRepository
    {
        bool ComprarEntrada(Entrada entrada);
        List<Entrada> ObtenerEntradasPorUsuario(int idUsuario);
        List<Entrada> ObtenerTodasLasEntradas();
    }

    public class EntradaRepository : IEntradaRepository
    {
        private PeliculasContext _db;

        public EntradaRepository(PeliculasContext db)
        {
            _db = db;
        }

        public bool ComprarEntrada(Entrada entrada)
        {
            try
            {
                var funcion = _db.Funciones.Where(x => !x.Borrado).FirstOrDefault(x => x.IdFuncion == entrada.IdFuncion);
                if (funcion == null)
                {
                    return false;
                }

                entrada.FechaCompra = DateTime.Now;
                _db.Entradas.Add(entrada);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Entrada> ObtenerTodasLasEntradas()
        {
            return _db.Entradas
                      .Include(x => x.Funcion)
                          .ThenInclude(x => x.Pelicula)
                      .Include(x => x.Usuario)
                      .OrderByDescending(x => x.FechaCompra)
                      .ToList();
        }

        public List<Entrada> ObtenerEntradasPorUsuario(int idUsuario)
        {
            return _db.Entradas
                      .Include(x => x.Funcion)
                          .ThenInclude(x => x.Pelicula)
                      .Where(x => x.IdUsuario == idUsuario)
                      .OrderByDescending(x => x.FechaCompra)
                      .ToList();
        }
    }


}
