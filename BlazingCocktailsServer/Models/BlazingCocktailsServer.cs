using Microsoft.EntityFrameworkCore;

namespace BlazingCocktailsServer;


public class Liquor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Cocktail>? Cocktails { get; set; }
}

public class Cocktail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Base { get; set; }
    public string Recipe { get; set; }
}
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Liquor> Liquors { get; set; }
    public DbSet<Cocktail> Cocktails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}


