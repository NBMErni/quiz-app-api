using System.ComponentModel.DataAnnotations;

namespace QuizAppAPI.Models.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
