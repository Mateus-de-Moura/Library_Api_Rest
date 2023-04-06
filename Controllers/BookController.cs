using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Api.Controllers
{
    [Route("/Book")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : ControllerBase
    {
        private readonly IClient _client;
        public BookController(IClient client)
        {
            _client = client;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetColectionAsync()
        {
            var livros = await _client.GetCollection();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public IActionResult GetLivroAsync(int Id)
        {
            var livros = _client.GetLivro(Id);
            return Ok(livros);
        }

        [HttpGet("Category/{id}")]
        public IActionResult GetLivrosPorCategoria(int Id)
        {
            var livros = _client.getPorCategoria(Id);
            return Ok(livros);
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] Livro Book)
        {
            _client.UpdateLivroAsync(Book);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteBook(int Id)
        {
            _client.DeleteLivro(Id);
            return Ok();
        }
    }
}
