using System.Collections.Generic;

namespace MoviesDemo.API.Models
{
    public class MoviesByYearResponse
    {
        public MoviesByYearResponse()
        {
            moviesByYear = new List<MoviesByYear>();
        }

        public List<MoviesByYear> moviesByYear { get; set; }
        public int total { get; set; }
    }
}
