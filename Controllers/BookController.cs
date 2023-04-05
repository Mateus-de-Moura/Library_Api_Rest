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

        [HttpPost("Cart")]
        public IActionResult AddCarrinho([FromBody] CarinhoCompra carrinho)
        {
            var quantidade = _client.AddCarrinho(carrinho);

            return Ok(quantidade);
        }

        [HttpGet("GetCart/{Id}")]
        public IActionResult GetAllItens(int Id)
        {
            var quantidade = _client.GetCarrinho(Id);
            return Ok(quantidade);
        }

        [HttpDelete("DeleteItemCart/{Id}")]
        public IActionResult DeleteItemCart(int Id)
        {
            _client.DeleteItenCart(Id);
            return Ok();
        }

        [HttpPut("UpdateAmount")] 
        public IActionResult UpdateQuantidade([FromBody] UpdateAmonth update)
        {
            _client.UpdateQuantIten(update.Id, update.Quantidade);
            return Ok();
        }

    }
}
