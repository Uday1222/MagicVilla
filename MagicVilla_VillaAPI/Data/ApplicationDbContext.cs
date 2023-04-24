using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Royal Villa Details",
                    ImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fik.imagekit.io%2F5tgxhsqev%2Fbackup-bucket%2Ftr%3Aw-800%2Ch-460%2Cq-62%2Fimage%2Fupload%2Fr2jetjmjhj5i6yijouhp.webp&tbnid=mawYSi6wALGu5M&vet=12ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg..i&imgrefurl=https%3A%2F%2Fwww.saffronstays.com%2Fvillas%2Fvillas-in-Nagaon%2520Beach%3Flat%3D18.563400%26lon%3D72.871400%26nearby%3Dtrue&docid=WBtW53B88S-OrM&w=800&h=460&q=villas&ved=2ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Royal Villa2",
                    Details = "Royal Villa Details2",
                    ImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fik.imagekit.io%2F5tgxhsqev%2Fbackup-bucket%2Ftr%3Aw-800%2Ch-460%2Cq-62%2Fimage%2Fupload%2Fr2jetjmjhj5i6yijouhp.webp&tbnid=mawYSi6wALGu5M&vet=12ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg..i&imgrefurl=https%3A%2F%2Fwww.saffronstays.com%2Fvillas%2Fvillas-in-Nagaon%2520Beach%3Flat%3D18.563400%26lon%3D72.871400%26nearby%3Dtrue&docid=WBtW53B88S-OrM&w=800&h=460&q=villas&ved=2ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                }
                ); ;
        }
    }
}
