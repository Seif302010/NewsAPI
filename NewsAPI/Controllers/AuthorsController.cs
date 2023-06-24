using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromForm] string? Name)
        {
            return Ok(await _authorsService.Get(Name));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] string Name)
        {
            Author author = new() { Name = Name };
           var result = await _authorsService.Post(author);
            if(result.Id==-1)
                return BadRequest("author already exist");
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromForm] string? Name)
        {
            var author = await _authorsService.GetById(id);
            if (author is null)
                return NotFound("author not found");
            else if (Name is null)
                return BadRequest("author not updated");

            author.Name = Name;
            author.updated_at = DateTime.Now;

            var result = _authorsService.Put(author);
            if (result.Id == -1)
                return BadRequest("there is an author with the same name");

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var author = await _authorsService.GetById(id);
            if (author is null)
                return NotFound("element not found");
            _authorsService.Delete(author);
            return Ok(author);
        }
    }
}
