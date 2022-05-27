using System.Net;
using System.Text;
using Movies.Dtos;
using Movies.Services;

namespace Movies.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Movies.Models;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Security.Claims;

    [ApiController]
    [Route("api/movies")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MovieController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;

        public MovieController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet("getMovie")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(await _cosmosDbService.GetMoviesAsync($"SELECT * FROM c WHERE c.userId = \"{userId}\""));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        
    /*
        [HttpGet("GetObjFromApi")]
        public string GetObjFromApi()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "https://api.themoviedb.org/3/discover/movie?api_key=c0d2b5193ae0ea4f12cccda56ceab5c1&sort_by=vote_average.desc&page=1&with_original_language=ko");
            request.Method = "GET";
            request.ContentType = "application/json:charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));

            string json = reader.ReadToEnd();

            reader.Close();
            stream.Close();
            response.Close();


            return json;

        }
    */

    [HttpPost("addMovie")]
        public async Task<ActionResult> CreateAsync([Bind("Id,UserId,Title,Description")] AddMovieDto addMovieDto)
        {
           
            if (ModelState.IsValid)
                try
                {
                    var movie = new Movie
                    {
                        Id = addMovieDto.Id,
                        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                        Title = addMovieDto.Title, 
                        Description = addMovieDto.Description,
                        Poster_image = addMovieDto.Poster_img,
                        Release_date = addMovieDto.Release_date,
                        Vote_average = addMovieDto.Vote_avg
                    };
            
                await _cosmosDbService.AddMovieAsync(movie);
                return Ok(movie);
                }
            
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            return BadRequest("Invalid movie model");
        }


        [HttpPut("id")]
        public async Task<ActionResult> EditAsync([Bind("Id,UserId,Title,Description")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _cosmosDbService.UpdateMovieAsync(movie.Id, movie);
                    return Ok(movie);

                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
            return BadRequest("Invalid movie model");
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Movie movie = await _cosmosDbService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _cosmosDbService.DeleteMovieAsync(id);
            return Ok();
        }


    }
}