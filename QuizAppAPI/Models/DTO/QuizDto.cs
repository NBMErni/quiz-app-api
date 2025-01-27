﻿using System.ComponentModel.DataAnnotations;

namespace QuizAppAPI.Models.DTO
{
    public class QuizDto
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

       
        public List<string> ListOfPossibleAnswers { get; set; } = new List<string>();

    }
}
