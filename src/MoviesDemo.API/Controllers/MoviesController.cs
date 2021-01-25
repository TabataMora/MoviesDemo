using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MoviesDemo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMovies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();
        const string BASE_URL = "https://jsonmock.hackerrank.com/api/movies/search?Title=";

        [HttpGet]
        public async Task<ActionResult<MoviesByYearResponse>> Get(string title)
        {
            var moviesByYearResponse = new MoviesByYearResponse();
            List<Movie> movies = new List<Movie>();

            try
            {
                ApiMovieReponse response = await RequestAPIMovies(title);

                if (response.data.Count > 0)
                {
                    movies.AddRange(response.data);

                    if (response.total_pages > 1)
                    {
                        for (int page = 2; page <= response.total_pages; page++)
                        {
                            response = await RequestAPIMovies(title, page);
                            movies.AddRange(response.data);
                        }
                    }
                }

                if (movies.Count > 0)
                {
                    IEnumerable<MoviesByYear> groupedMovies = movies.GroupBy(m => m.Year)
                    .OrderBy(group => group.Key)
                    .Select(group => new MoviesByYear(group.Key, group.Count()));

                    moviesByYearResponse.moviesByYear.AddRange(groupedMovies);

                    foreach (var item in moviesByYearResponse.moviesByYear)
                    {
                        moviesByYearResponse.total = moviesByYearResponse.total + item.movies;
                    }

                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a requisição: " + e.Message);
            }

            return Ok(moviesByYearResponse);
        }

        private async Task<ApiMovieReponse> RequestAPIMovies(string title, int page = 1)
        {
            ApiMovieReponse responseObject = null;
            string pageFilter = $"&page={page}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(BASE_URL + title + pageFilter);
                if (response.IsSuccessStatusCode)
                {
                    string responseTextAPI = await response.Content.ReadAsStringAsync();
                    responseObject = JsonSerializer.Deserialize<ApiMovieReponse>(responseTextAPI);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return responseObject;
        }
    }
}
