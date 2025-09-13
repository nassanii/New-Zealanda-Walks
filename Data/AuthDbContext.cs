using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalks.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReaderRoleID = "81665b38-0c97-4a41-a309-85c5dafbafde";
            var WriterRoleID = "2949f8d1-e950-4e8c-a7f1-88a4f2f949f7";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReaderRoleID,
                    ConcurrencyStamp = ReaderRoleID,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()

                },

                new IdentityRole
                {
                    Id = WriterRoleID,
                    ConcurrencyStamp = WriterRoleID,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()

                }

            };

            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
