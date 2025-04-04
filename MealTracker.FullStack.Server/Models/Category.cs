using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MealAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();// Navigation property for related meals
    }
}
