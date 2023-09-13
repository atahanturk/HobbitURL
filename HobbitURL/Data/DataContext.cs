using HobbitURL.Models;
using Microsoft.EntityFrameworkCore;

namespace HobbitURL.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    public DbSet<ShortenedUrlModel> ShortenedUrls { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrlModel>()
            .HasKey(pc => new { pc.Id });
        modelBuilder.Entity<ShortenedUrlModel>().HasIndex(s => s.ShortUrl).IsUnique();
        

    }
    
    

    
    
}