using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Mappers;
using MovieDatabaseAPI.Models;

namespace MovieDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetMovies()
        {
            return await _context.Movies.Select(m => m.ToMovieListDto()).ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var result = await _context.Movies
                .Include(m => m.Reviews.OrderByDescending(r => r.CreatedAt).Take(3))
                .ThenInclude(r => r.User)
                .Select(m => new
                {
                    Movie = m,
                    AverageRating = m.Reviews.DefaultIfEmpty().Average(r => r == null ? 0 : r.Rating)
                })
                .FirstOrDefaultAsync(m => m.Movie.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return result.Movie.ToMovieDto(result.AverageRating);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie.ToMovie()).State = EntityState.Modified;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movieCreateDto)
        {
            Movie movie = movieCreateDto.ToMovie();
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie.ToMovieDto());
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
