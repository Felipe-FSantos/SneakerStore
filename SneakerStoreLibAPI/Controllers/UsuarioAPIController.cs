using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SneakerStoreLib.Entidades;
using SneakerStoreAPI.Model;

namespace SneakerStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsuarioAPIController : ControllerBase
    {
        

        /// <summary>
        ///  Controller para retornar um objeto usuário
        ///  Nesse método após a autenticação é gerado o token JWT para acesso aos outros métodos da API
        /// </summary>
        /// <param user="usuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("retUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> retUsuario(string username, string pwd)
        {
            if (username == null || pwd == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }

            Usuario usuario = BaseModel.ObterUsuario(username, pwd);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuário ou senha não encontrados !" });
            }
            else
            {
                //Gera token JWT
                string token = string.Empty;

                token = Model.Token.TokenService.GenerateToken(usuario);

                return Ok(new
                {
                    userInfo = usuario,
                    tokenJwt = token
                });
            }
        }

    }
}
