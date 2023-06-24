using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly INewsService _newsService;
        private readonly IAuthorsService _authorsService;

        public NewsController(IMapper mapper, INewsService newsService, IAuthorsService authorsService)
        {
            _mapper = mapper;
            _newsService = newsService;
            _authorsService = authorsService;
        }

        private new List<string> _allowedExtensions = new()
        {
            ".jpg",".png"
        };

        private static long _sizeinMB = 5;

        private long _maxAllowedImageSize = _sizeinMB * 1024 * 1024;

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult>  GetAsync([FromForm] string? Title, [FromForm] string? Author, [FromForm] DateTime? dateTime) {
            return Ok(_newsService.Map(await _newsService.Get(Title, Author, dateTime)));
        }

        [HttpGet("published")]
        public async Task<IActionResult> PublishedAsync([FromForm] string? Title, [FromForm] string? authorName)
        {
            return Ok(_newsService.Map(await _newsService.Get(Title, authorName, DateTime.Now)));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] NewsDto dto) {
            var isValidAuthor = await _authorsService.GetById(dto.AuthorId);
            if (isValidAuthor is null)
                return NotFound("Author not found");
            News news = _mapper.Map<News>(dto);
            if (dto.image != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.image.FileName).ToLower()))
                    return BadRequest("jpg and png only");
                if (dto.image.Length > _maxAllowedImageSize)
                    return BadRequest($"The max allowed size is {_sizeinMB} MB");

                using var dataStream = new MemoryStream();
                await dto.image.CopyToAsync(dataStream);
                news.image = dataStream.ToArray();
            }
          var result= await _newsService.Post(news);
            return Ok(_mapper.Map<NewsDetailsDto>(result));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromForm] NewsPutDto dto) {
            News news = await _newsService.GetById(id);
            if (news is null)
                return NotFound("News not found");
            else if (dto.AuthorId != 0 && await _authorsService.GetById(dto.AuthorId) is null)
                return NotFound("Author not found");

            news.Title = dto.Title != null ? dto.Title : news.Title;
            news.Content = dto.Content != null ? dto.Content : news.Content;
            news.AuthorId = dto.AuthorId != 0 ? dto.AuthorId : news.AuthorId;
            news.publication_date = (DateTime)(dto.publication_date != null ? dto.publication_date : news.publication_date);
            if (dto.image != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.image.FileName).ToLower()))
                    return BadRequest("jpg and png only");
                if (dto.image.Length > _maxAllowedImageSize)
                    return BadRequest($"The maximum allowed size is {_sizeinMB} MB");

                using var dataStream = new MemoryStream();
                await dto.image.CopyToAsync(dataStream);
                news.image = dataStream.ToArray();
            }
            var result = _newsService.Put(news);
            return Ok(_mapper.Map<NewsDetailsDto>(result));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            News news = await _newsService.GetById(id);
            if (news is null)
                return NotFound("News not found");

            var result = _newsService.Delete(news);
            return Ok(_mapper.Map<NewsDetailsDto>(result));
        }
    }
}
