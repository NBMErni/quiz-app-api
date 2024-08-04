using EMPManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var user = dbContext.User.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            /*dbContext.User.FirstOrDefault(x => x.Username == userDto.Username && x.Password == userDto.Password);*/
            var user = dbContext.User
                      .Where(x => EF.Functions.Collate(x.Username, "Latin1_General_BIN") == userDto.Username &&
                                  EF.Functions.Collate(x.Password, "Latin1_General_BIN") == userDto.Password)
                      .FirstOrDefault();


            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            return Ok(new
            {
                Message = "Login successful",
                Role = user.Role,
                User = user.Username
             
            });
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
                Username = registerDto.Username,
                Password = registerDto.Password,
                Role = "user" 
            };

            dbContext.User.Add(user);
            dbContext.SaveChanges();

            var response = new
            {
                Message = $"User {user.Username} has been successfully created."
            };

            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.Username }, response);
        }
    }
}