using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.BluRays
{
    public class BluRaysRepository : IBluRaysRepository
    {
        private readonly VideothequeDb _db;

        public BluRaysRepository(VideothequeDb db)
        {
            _db = db;
        }
        public Task<List<BluRayDto>> GetBluRays() => _db.BluRays.ToListAsync();

        public ValueTask<BluRayDto?> GetBluRay(int id) => _db.BluRays.FindAsync(id);
    
        public Task InsertFilm(FilmDto film)
        {
            var bluRay = new BluRayDto
            {
                Id = film.Id,
                Title = film.Titre,
                Duration = film.Duree,
                IdFirstActor = film.ActeurPrincipal.Id,
                IdDirector = film.Realisateur.Id,
                IdScenarist = film.Scenariste.Id,
                IdAgeRating = film.AgeRating.Id,
                IdGenre = film.Genre.Id
            };

            _db.BluRays.AddAsync(bluRay);
            return _db.SaveChangesAsync();
        }
    }
}
