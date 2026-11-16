using Cine.Data.Context;
using Cine.data.Modelos;
using Cine.Data.Context;
using Cine.Data.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cine.data.Repos
{

    public interface ISalaRepository
    {
        List<Sala> ObtenerSalas();
        Sala? ObtenerPorID(int id);
        bool AgregarSala(Sala sala);
        void ActualizarSala(Sala sala);
        bool EliminarSala(int id);
    }
    public class SalaRepository : ISalaRepository
    {

        private PeliculasContext _db;

        public SalaRepository(PeliculasContext context)
        {
            _db = context;
        }

        public void ActualizarSala(Sala sala)
        {
            _db.Salas.Update(sala);
            _db.SaveChanges();
        }

        public bool AgregarSala(Sala sala)
        {
            try
            {
                _db.Salas.Add(sala);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarSala(int id)
        {
            try
            {
                Sala sala = _db.Salas.Find(id);
                if (sala != null)
                {
                    _db.Salas.Remove(sala);
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

        public Sala? ObtenerPorID(int id)
        {
            Sala sala = _db.Salas.Find(id);
            return sala;
        }

        public List<Sala> ObtenerSalas()
        {
            return _db.Salas.Where(x => !x.Borrado).ToList();
        }
    }
}
