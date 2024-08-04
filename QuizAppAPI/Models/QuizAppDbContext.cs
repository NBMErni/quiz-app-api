using Microsoft.EntityFrameworkCore;
using QuizAppAPI.Controllers;
using QuizAppAPI.Models.Domain;

namespace EMPManagementAPI.Models
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options) { }

        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the default collation for the database
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

            // Additional model configurations can go here
        }
    }
}
