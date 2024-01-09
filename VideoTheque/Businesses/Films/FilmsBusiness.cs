using System;
using System.Threading.Tasks;
using VideoTheque.DTOs;
using VideoTheque.Repositories.BluRays;
using VideoTheque.Repositories.AgeRatings;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Businesses.Films;
using VideoTheque.Core;
using System.IO;

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
                    Realisateur = director,
                    Scenariste = scenarist,
                    Duree = (int)bluRay.Duration,
                    Support = "Blu-Ray",
                    AgeRating = ageRating,
                    Genre = genre,
                    Titre = bluRay.Title,
                    ActeurPrincipal = mainActor
                });
            }

            return filmDtos;
        }

        public FilmDto GetFilm(int id)
        {
            var bluRay = _bluRaysRepository.GetBluRay(id).Result;
            if (bluRay == null) throw new KeyNotFoundException("BluRay not found");

            var director = _personnesRepository.GetPersonne(bluRay.IdDirector).Result;
            var scenarist = _personnesRepository.GetPersonne(bluRay.IdScenarist).Result;
            var mainActor = _personnesRepository.GetPersonne(bluRay.IdFirstActor).Result;
            var genre = _genresRepository.GetGenre(bluRay.IdGenre).Result;
            var ageRating = _ageRatingsRepository.GetAgeRating(bluRay.IdAgeRating).Result;

            return new FilmDto
            {
                Id = id,
                Realisateur = director,
                Scenariste = scenarist,
                Duree = (int)bluRay.Duration,
                Support = "Blu-Ray",
                AgeRating = ageRating,
                Genre = genre,
                Titre = bluRay.Title,
                ActeurPrincipal = mainActor
            };
        }

        public FilmDto InsertFilm(FilmDto film)
        {
            if (_bluRaysRepository.InsertFilm(film).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion de {film.Titre}");
            }

            return film;
        }
    }
}