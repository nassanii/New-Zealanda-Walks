
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models;

namespace NZwalks.API.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Difficulty> difficulties { get; set; }
    public DbSet<Region> regions { get; set; }
    public DbSet<Walk> walks { get; set; }
    public DbSet<Image> images { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for dificulties
        // Easy , Medium , Hard

        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("23c7cb1e-1632-43a4-8857-6da05c0b73af") ,
                Name = "Easy"
            },
             new Difficulty()
            {
                Id = Guid.Parse("52ba1ea3-3eb6-4de9-86a0-7d5ed35569d6") ,
                Name = "Medium"
            },
              new Difficulty()
            {
                Id = Guid.Parse("df19a7ef-63a7-43b6-86d8-346f84e9ca6e") ,
                Name = "Hard"
            }
        };

        // Seed data to the database 
        modelBuilder.Entity<Difficulty>().HasData(difficulties);

        // Seed data for Regions
        var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImgURL = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImgURL = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImgURL = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImgURL = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImgURL = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImgURL = null
                },
            };

        modelBuilder.Entity<Region>().HasData(regions);

    }

}