using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MealAPI.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Measure { get; set; } // e.g., "grams", "cups", "tablespoons"

        public decimal Amount { get; set; } // e.g., 100, 0.5, 2

        // Foreign key to Meal
        public int MealId { get; set; }

        // Navigation property to Meal
        [JsonIgnore] // Exclude this property from serialization
        public Meal? Meal { get; set; }
    }
}
