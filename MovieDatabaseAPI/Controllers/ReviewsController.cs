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
    public class ReviewsController : ControllerBase
    {
        private readonly DataContext _context;

        public ReviewsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewListDto>>> GetReviews()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Movie)
                .Select(r => r.ToReviewListDto())
                .ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return review.ToReviewDto();
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewUpdateDto reviewUpdateDto)
        {
            if (id != reviewUpdateDto.Id)
            {
                return BadRequest();
            }
            
            var existingReview = await _context.Reviews.FindAsync(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            var review = reviewUpdateDto.ToReview();
            review.CreatedAt = existingReview.CreatedAt;

            _context.Entry(review).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostReview(ReviewCreateDto reviewCreateDto)
        {
            var review = reviewCreateDto.ToReview();
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            
            var newReview = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == review.Id);

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, newReview!.ToReviewDto());
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
