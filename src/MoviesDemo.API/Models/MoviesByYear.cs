namespace MoviesDemo.API.Models
{
    public class MoviesByYear
    {
        public MoviesByYear()
        {

        }
        public MoviesByYear(int year, int movies)
        {
            this.year = year;
            this.movies = movies;
        }

        public int year { get; set; }
        public int movies { get; set; }
    }
}
