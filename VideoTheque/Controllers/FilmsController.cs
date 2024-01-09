using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Films;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmsBusiness _filmsBusiness;
        protected readonly ILogger<FilmsController> _logger;

        public FilmsController(ILogger<FilmsController> logger, IFilmsBusiness filmsBusiness)
        {
            _logger = logger;
            _filmsBusiness = filmsBusiness;
        }

        [HttpGet]
        public async Task<List<FilmViewModel>> GetFilms() => (await _filmsBusiness.GetFilms()).Adapt<List<FilmViewModel>>();

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetFilm([FromRoute] int id) => _filmsBusiness.GetFilm(id).Adapt<FilmViewModel>();
    }
}
