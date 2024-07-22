using EMPManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAppAPI.Models.Domain;
using QuizAppAPI.Models.DTO;

namespace QuizAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAppController : ControllerBase
    {
        private readonly QuizAppDbContext dbContext;

        public QuizAppController(QuizAppDbContext dbContext)
        {
          
            this.dbContext = dbContext;
        }

        [HttpGet]

        public IActionResult GetAllQuestions()
        {
            var quizzes = dbContext.Quiz.ToList();

            return Ok(quizzes);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetQuestionById ([FromRoute] int id)
        {
            var quiz = dbContext.Quiz.FirstOrDefault(x => x.QuizId == id);

            return Ok(quiz);
        }

        [HttpPost]
        public IActionResult CreateQuestion(QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quiz = new Quiz
                {
                    Question = quizDto.Question,
                    Answer = quizDto.Answer,
                    ListOfPossibleAnswers = quizDto.ListOfPossibleAnswers
                };

                dbContext.Quiz.Add(quiz);
                dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetQuestionById), new { id = quiz.QuizId }, quiz);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/EmployeeInformation/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] QuizDto updateQuiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = dbContext.Quiz.FirstOrDefault(x => x.QuizId == id);

            if (quiz == null)
            {
                return NotFound();
            }

            try
            {
                quiz.Question = updateQuiz.Question;
                quiz.Answer = updateQuiz.Answer;

                dbContext.SaveChanges();
                var response = new
                {
                    Message = $"Questionnaire with ID {quiz.QuizId} has been successfully updated."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }



        // DELETE: api/EmployeeInformation
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var quiz = await dbContext.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            dbContext.Quiz.Remove(quiz);
            await dbContext.SaveChangesAsync();

            return Ok($"Questionnaire with ID {id} has been deleted");
        }

    }
}
