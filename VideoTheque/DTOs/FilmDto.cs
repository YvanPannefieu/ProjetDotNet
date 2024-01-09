namespace VideoTheque.DTOs
{
    public class FilmDto
    {
        public int Id { get; set; }
        public PersonneDto Realisateur { get; set; }
        public PersonneDto Scenariste { get; set; }
        public int Duree { get; set; }
        public string Support { get; set; }
        public AgeRatingDto AgeRating { get; set; }
        public GenreDto Genre { get; set; }
        public string Titre { get; set; }
        public PersonneDto ActeurPrincipal { get; set; }
    }
}
