using System.Collections.Generic;

namespace MoviesDemo.API.Models
{
    public class ApiMovieReponse
    {
        public string page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Movie> data { get; set; }
    }

}

