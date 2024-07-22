
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
    }
}
