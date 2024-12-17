using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Cors;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]

    public class UsersController : ControllerBase
    {
        private readonly IUsers _UsersRepository;

        public UsersController(IUsers UsersRepository)
        {
            _UsersRepository = UsersRepository;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> getAllUsers()
        {
            var items = await _UsersRepository.GetAllUsersAsync(); // Await repository method
            return Ok(items); // Return the result as HTTP 200 OK
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> GetUserByCredentials([FromBody] Users userLoginDto)
        {
            // Validate the input
            if (string.IsNullOrEmpty(userLoginDto.Email) || string.IsNullOrEmpty(userLoginDto.Password))
            {
                return BadRequest( "Email and Password are required.");
            }

            var user = await _UsersRepository.GetUserByCredentialsAsync(userLoginDto.Email, userLoginDto.Password);

            if (user == null)
            {
                return NotFound(new { label = "Invalid email or password", valid = false });

            }
            return Ok(new { UserDetail = new { user, label = "Login Successfully",valid=true } });

        }


        [HttpPost]
        public async Task<ActionResult<Users>> CreateUsers([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _UsersRepository.AddUsersAsync(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }



        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Users Users)
        {
            if (id != Users.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _UsersRepository.UpdateUsersAsync(Users);
            return Ok(updatedItem);
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _UsersRepository.DeleteItemAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        //[HttpPost]
        //public async Task<IActionResult> LoginUser([FromBody] Users user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest(new { error = "Invalid request" });
        //    }

        //    var data = await _UsersRepository.GetUserByEmail(user.Email);
        //    if (data == null)
        //    {
        //        return BadRequest(new { error = "Email does not exist" });
        //    }

        //    if (data.Password != user.Password)
        //    {
        //        return BadRequest(new { error = "Email/Password incorrect" });
        //    }

        //    return Ok(data);
        //}


    }
}
