using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Ballte> Balltes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=EFCore;Trusted_Connection=True;"
        );

        // dotnet ef migrations add InitialCreate
        // dotnet ef database drop
        // dotnet ef database update
        // dotnet ef migrations remove
        // dotnet ef database update LastGoodMigration
        // dotnet ef migrations script
        // myDbContext.Database.Migrate();
    }
}