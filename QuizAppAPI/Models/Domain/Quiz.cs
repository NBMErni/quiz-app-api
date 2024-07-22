using QuizAppAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace QuizAppAPI.Models.Domain
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public List<string> ListOfPossibleAnswers { get; set; } = new List<string>();
    }
}
