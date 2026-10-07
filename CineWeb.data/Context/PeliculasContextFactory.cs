using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cine.Data.Context
{
    public class PeliculasContextFactory : IDesignTimeDbContextFactory<PeliculasContext>
    //tuve que crear esto porque sino no me dejaba añadir las migraciones iniciales.
    {
        public PeliculasContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PeliculasContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CineDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new PeliculasContext(optionsBuilder.Options);
        }
    }
}