using EMPManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAppAPI.Models.Domain;
using QuizAppAPI.Models.DTO;
using System.Diagnostics;
using System.Linq;

namespace QuizAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QuizAppDbContext dbContext;

        public UserController(QuizAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = dbContext.User.ToList();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            Debug.WriteLine($"Username: {username}");
            var user = dbContext.User.FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            var user = dbContext.User.FirstOrDefault(x => x.UserName == userDto.UserName && x.Password == userDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Message = "Login successful", User = user.Role });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Role = "user" 
            };

            dbContext.User.Add(user);
            dbContext.SaveChanges();

            var response = new
            {
                Message = $"User {user.UserName} has been successfully created."
            };

            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.UserName }, response);
        }
    }
}