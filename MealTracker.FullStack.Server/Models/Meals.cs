using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MealAPI.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? CategoryId { get; set; } // Foreign key to Categories

        [JsonIgnore]
        public Category? Category { get; set; }// Navigation property to Category

        public DateTime? CreatedAt { get; set; } // Default value set in the database

        // Navigation property for Ingredients
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    }

}
