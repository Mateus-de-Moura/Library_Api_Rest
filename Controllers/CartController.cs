using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Api.Controllers
{
    [Route("/Cart")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
        private readonly ICart _cart;
        public CartController(ICart cart)
        {
            _cart = cart;
        }

        [HttpGet("GetCart/{Id}")]
        public IActionResult GetAllItens(int Id)
        {
            var quantidade = _cart.GetCarrinho(Id);
            return Ok(quantidade);
        }

        [HttpPut("UpdateAmount")]
        public IActionResult UpdateQuantidade([FromBody] UpdateAmonth update)
        {
            _cart.UpdateQuantIten(update.Id, update.Quantidade);
            return Ok();
        }
    
        [HttpPost("AddNewIten")]
        public IActionResult AddCarrinho([FromBody] CarinhoCompra carrinho)
        {
            var quantidade = _cart.AddCarrinho(carrinho);

            return Ok(quantidade);
        }

        [HttpDelete("DeleteItemCart/{Id}")]
        public IActionResult DeleteItemCart(int Id)
        {
            _cart.DeleteItenCart(Id);
            return Ok();
        }


    }
}
