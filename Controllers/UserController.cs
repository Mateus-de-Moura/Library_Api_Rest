    using Biblioteca_Api.Interfaces;
using Biblioteca_Api.Models;
using Biblioteca_Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Api.Controllers
{

    [Route("User/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IAuth _Auth;
        public UserController(IAuth auth)
        {
            _Auth = auth;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUser(int Id)
        {
           var user = await _Auth.GetUser(Id);
            if (user != null)
            {
                return Ok(user.Base64Photo);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> AuthUser([FromBody] UserLogin user)
        {
            user.Senha = Criptografia.Encript(user.Senha);
            int retorno = await _Auth.loginAsync(user);

            if (retorno != 0) {
                string token = TokenService.GenerateToken(user.Email, user.Senha);
                return Ok("token: \n" + token + "\n:" + retorno);
            }
            else
            {
                return NotFound();
            }
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Registre(UserModel user)
        {
            user.Senha = Criptografia.Encript(user.Senha);
            var retorno = await _Auth.RegistreAsync(user);

            if (retorno.Item1 == "Usuario cadastrado")
            {
                return Ok(retorno.Item2);
            }
            else
            {
                return BadRequest(retorno.Item1);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> EditUser([FromBody] UserModel user)
        {
            var retorno = await _Auth.UpdateAsync(user);
            if (retorno)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var retorno = await _Auth.DeleteUserAsync(Id);
            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
    }
}
