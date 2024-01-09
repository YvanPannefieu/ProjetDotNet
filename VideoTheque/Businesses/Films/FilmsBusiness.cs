using System;
using System.Threading.Tasks;
using VideoTheque.DTOs;
using VideoTheque.Repositories.BluRays;
using VideoTheque.Repositories.AgeRatings;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Businesses.Films;

namespace VideoTheque.Business
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IBluRaysRepository _bluRaysRepository;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IGenresRepository _genresRepository;
        private readonly IAgeRatingsRepository _ageRatingsRepository;

        public FilmsBusiness(IBluRaysRepository bluRayRepository, IPersonnesRepository personnesRepository,
                            IGenresRepository genresRepository, IAgeRatingsRepository ageRatingsRepository)
        {
            _bluRaysRepository = bluRayRepository;
            _personnesRepository = personnesRepository;
            _genresRepository = genresRepository;
            _ageRatingsRepository = ageRatingsRepository;
        }

        public async Task<List<FilmDto>> GetFilms()
        {
            var bluRays = await _bluRaysRepository.GetBluRays();
            var filmDtos = new List<FilmDto>();

            foreach (var bluRay in bluRays)
            {
                var director = await _personnesRepository.GetPersonne(bluRay.IdDirector);
                var scenarist = await _personnesRepository.GetPersonne(bluRay.IdScenarist);
                var mainActor = await _personnesRepository.GetPersonne(bluRay.IdFirstActor);
                var genre = await _genresRepository.GetGenre(bluRay.IdGenre);
                var ageRating = await _ageRatingsRepository.GetAgeRating(bluRay.IdAgeRating);

                filmDtos.Add(new FilmDto
                {
                    Id = bluRay.Id,
                    Realisateur = director != null ? $"{director.FirstName} {director.LastName}" : "Unknown",
                    Scenariste = scenarist != null ? $"{scenarist.FirstName} {scenarist.LastName}" : "Unknown",
                    Duree = (int)bluRay.Duration,
                    Support = "Blu-Ray",
                    AgeRating = ageRating != null ? ageRating.Name : "Unknown",
                    Genre = genre != null ? genre.Name : "Unknown",
                    Titre = bluRay.Title,
                    ActeurPrincipal = mainActor != null ? $"{mainActor.FirstName} {mainActor.LastName}" : "Unknown"
                });
            }

            return filmDtos;
        }

        public async Task<FilmDto> GetFilm(int bluRayId)
        {
            var bluRay = await _bluRaysRepository.GetBluRay(bluRayId);
            if (bluRay == null) throw new KeyNotFoundException("BluRay not found");

            var director = await _personnesRepository.GetPersonne(bluRay.IdDirector);
            var scenarist = await _personnesRepository.GetPersonne(bluRay.IdScenarist);
            var mainActor = await _personnesRepository.GetPersonne(bluRay.IdFirstActor);
            var genre = await _genresRepository.GetGenre(bluRay.IdGenre);
            var ageRating = await _ageRatingsRepository.GetAgeRating(bluRay.IdAgeRating);
            
            return new FilmDto
            {
                Id = bluRayId,
                Realisateur = director != null ? $"{director.FirstName} {director.LastName}" : "Unknown",
                Scenariste = scenarist != null ? $"{scenarist.FirstName} {scenarist.LastName}" : "Unknown",
                Duree = (int)bluRay.Duration,
                Support = "Blu-Ray",
                AgeRating = ageRating != null ? ageRating.Name : "Unknown",
                Genre = genre != null ? genre.Name : "Unknown",
                Titre = bluRay.Title,
                ActeurPrincipal = mainActor != null ? $"{mainActor.FirstName} {mainActor.LastName}" : "Unknown"
            };
        }
    }
}