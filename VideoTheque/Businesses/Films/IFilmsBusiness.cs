 using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FilmDto>> GetFilms();

        Task<FilmDto> GetFilm(int id);

    }
}
