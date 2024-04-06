using Course_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Course_project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=helloapp.db");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "sasha",
                    UserName = "sasha",
                    Email = "sirlolka@gmail.com"

                });
            base.OnModelCreating(builder);
        }

        public DbSet<Collection> Collections { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagConnection> TagConnections { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<StyleConnections> StyleConnections { get; set; }
    }
}
