using MealAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly MealDbContext _context;

        public MealsController(MealDbContext context)
        {
            _context = context;
        }

        // GET: api/meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            // Include the related Category and Ingredients data
            var meals = await _context.Meals
                .Include(m => m.Category) // Eagerly load the Category
                .Include(m => m.Ingredients) // Eagerly load the Ingredients
                .ToListAsync();

            if (meals == null || !meals.Any())
            {
                return NotFound("No meals found in the database.");
            }

            return Ok(meals);
        }

        // GET: api/meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            // Include the related Category and Ingredients data
            var meal = await _context.Meals
                .Include(m => m.Category) // Eagerly load the Category
                .Include(m => m.Ingredients) // Eagerly load the Ingredients
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meal == null)
            {
                return NotFound("Meal not found.");
            }

            return Ok(meal);
        }

        // POST: api/meals
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(Meal meal)
        {
            // Validate that the Category exists
            if (meal.CategoryId != null)
            {
                var category = await _context.Categories.FindAsync(meal.CategoryId);
                if (category == null)
                {
                    return BadRequest("Invalid CategoryId. The specified category does not exist.");
                }
            }

            meal.CreatedAt = DateTime.UtcNow; // Set the CreatedAt field
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeal), new { id = meal.Id }, meal);
        }

        // PUT: api/meals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, Meal meal)
        {
            if (id != meal.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }

            // Find the existing meal in the database
            var existingMeal = await _context.Meals.FindAsync(id);
            if (existingMeal == null)
            {
                return NotFound("Meal not found.");
            }

            // Validate that the Category exists
            if (meal.CategoryId != null)
            {
                var category = await _context.Categories.FindAsync(meal.CategoryId);
                if (category == null)
                {
                    return BadRequest("Invalid CategoryId. The specified category does not exist.");
                }
            }

            // Update only the allowed fields
            existingMeal.Name = meal.Name;
            existingMeal.Description = meal.Description;
            existingMeal.CategoryId = meal.CategoryId;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflicts
                if (!_context.Meals.Any(e => e.Id == id))
                {
                    return NotFound("Meal not found.");
                }
                else
                {
                    throw; // Re-throw the exception if it's not a "not found" scenario
                }
            }

            return NoContent(); // 204 No Content indicates success with no response body
        }

        // DELETE: api/meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound("Meal not found.");
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
