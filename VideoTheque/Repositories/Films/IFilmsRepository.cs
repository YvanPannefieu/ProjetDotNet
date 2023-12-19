using System.Threading.Tasks;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Films
{
    public interface IFilmsRepository
    {
        Task<List<FilmDto>> GetFilms();
        
        ValueTask<FilmDto?> GetFilm(int id);

        Task InsertFilm(FilmDto film);

        Task UpdateFilm(int id, FilmDto film);

        Task DeleteFilm(int id);
    }
}
