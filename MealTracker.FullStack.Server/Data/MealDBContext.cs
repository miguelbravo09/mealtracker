using Microsoft.EntityFrameworkCore;
using MealAPI.Models;
public class MealDbContext : DbContext
{
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Category> Categories { get; set; } // Add this line

    public MealDbContext(DbContextOptions<MealDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the relationship between Meal and Category
        modelBuilder.Entity<Meal>()
            .HasOne(m => m.Category) // Meal has one Category
            .WithMany(c => c.Meals) // Category has many Meals
            .HasForeignKey(m => m.CategoryId) // Foreign key in Meal
            .OnDelete(DeleteBehavior.SetNull); // Optional: Set CategoryId to null if Category is deleted
        
        // Configure the relationship between Meal and Ingredient
        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.Meal)
            .WithMany(m => m.Ingredients)
            .HasForeignKey(i => i.MealId)
            .OnDelete(DeleteBehavior.Cascade); // Delete ingredients when a meal is deleted

        base.OnModelCreating(modelBuilder);
    }
}