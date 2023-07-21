using Microsoft.EntityFrameworkCore;
using Task.Api.Models;

namespace Task.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();

        //add default data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = Guid.NewGuid(), CategoryName = "smartphone", RangeFrom = 3, RangeTo = 9, Percent = 3 },
            new Category { Id = Guid.NewGuid(), CategoryName = "computer", RangeFrom = 3, RangeTo = 12, Percent = 4 },
            new Category { Id = Guid.NewGuid(), CategoryName = "tv", RangeFrom = 3, RangeTo = 18, Percent = 5}
        );
    }
}