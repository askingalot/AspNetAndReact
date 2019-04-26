using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetAndReact.Data;
using AspNetAndReact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspNetAndReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetMovie()
        {
            List<Movie> movies =  
                await _context.Movie
                        .Include(m => m.UserMovies)
                        .ToListAsync();
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            return Ok(
                movies.Select(m => new MovieViewModel {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    IsUserFavorite = m.UserMovies.Any(um => um.UserId == user.Id)
                })
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieViewModel movieVm)
        {
            if (id != movieVm.Id)
            {
                return BadRequest();
            }

            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            Movie movieFromDb =
                await _context.Movie
                        .Include(m => m.UserMovies)
                        .FirstOrDefaultAsync(m => m.Id == id);

            if (movieFromDb == null)
            {
                return NotFound();
            }

            bool isCurrentFavorite = movieFromDb.UserMovies.Any(um => um.UserId == user.Id);
            if (movieVm.IsUserFavorite && !isCurrentFavorite)
            {
                UserMovie userMovie = new UserMovie
                {
                    UserId = user.Id,
                    MovieId = id
                };
                _context.Add(userMovie);
            }
            else if (! movieVm.IsUserFavorite && isCurrentFavorite)
            {
                UserMovie userMovie = movieFromDb.UserMovies.First(um => um.UserId == user.Id);
                _context.UserMovie.Remove(userMovie);
            }

            movieFromDb.Title = movieVm.Title;
            movieFromDb.Year = movieVm.Year;
            _context.Update(movieFromDb);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
